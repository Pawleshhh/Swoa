using Astronomy;
using CelestialObjects;
using SwoaDatabaseAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using static Utilities.PropertyChangedHelper;

namespace Swoa
{
    public class CelestialObjectManager : IAsyncTaskDirector
    {

        #region Constructors
        public CelestialObjectManager(ICelestialObjectCollection celestialObjects, SwoaDb swoaDb, IDateTimeService dateTimeService)
        {
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));
            this.dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));

            TimeMachine = new TimeMachine(dateTimeService);

            TimeMachine.DateChanged += TimeMachine_DateChanged;
            TimeMachine.LongitudeChanged += TimeMachine_LongitudeChanged;
            TimeMachine.LatitudeChanged += TimeMachine_LatitudeChanged;
            TimeMachine.SetCurrentDateChanged += TimeMachine_SetCurrentDateChanged;
        }

        #endregion

        #region Fields

        private readonly ICelestialObjectCollection celestialObjects;
        private readonly SwoaDb swoaDb;
        private readonly IDateTimeService dateTimeService;

        private bool changeOnUpdateEnabled = true;
        private bool isWorking;

        private CancellationTokenSource? tokenSource;
        private Task? updateCurrentMapTask;
        private object lockLoadingMap = new object();

        #endregion

        #region Properties

        public IReadOnlyCollection<CelestialObject> CelestialObjects => celestialObjects;

        public TimeMachine TimeMachine { get; }

        public bool ChangeOnUpdateEnabled
        {
            get => changeOnUpdateEnabled;
            set => SetProperty(ref changeOnUpdateEnabled, value, OnChangeOnUpdateEnabledChanged);
        }

        public bool IsWorking
        {
            get => isWorking;
            set => SetProperty(ref isWorking, value, OnIsWorkingChanged);
        }

        #endregion

        #region Events

        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Added
        {
            add => celestialObjects.Added += value;
            remove => celestialObjects.Added -= value;
        }
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Removed
        {
            add => celestialObjects.Removed += value;
            remove => celestialObjects.Removed -= value;
        }
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Cleared
        {
            add => celestialObjects.Cleared += value;
            remove => celestialObjects.Cleared -= value;
        }

        public event EventHandler<DataChangedEventArgs<bool>>? ChangeOnUpdateEnabledChanged;
        public event EventHandler<DataChangedEventArgs<bool>>? IsWorkingChanged;

        protected void OnChangeOnUpdateEnabledChanged(bool prev, bool curr)
            => ChangeOnUpdateEnabledChanged?.Invoke(this, new DataChangedEventArgs<bool>(prev, curr));
        protected void OnIsWorkingChanged(bool previous, bool current)
            => IsWorkingChanged?.Invoke(this, new DataChangedEventArgs<bool>(previous, current));

        #endregion

        #region Methods

        public bool Add(CelestialObject celestialObject)
        {
            celestialObjects.Add(celestialObject);

            return true;
        }

        public bool Remove(CelestialObject celestialObject)
        {
            return celestialObjects.Remove(celestialObject);
        }

        public void Clear()
        {
            celestialObjects.Clear();
        }

        public bool CanBeAdded(CelestialObject celestialObject)
        {
            return celestialObject != null;
        }

        private void OnAnyPropertyChanged(bool wait = false)
        {
            if (ChangeOnUpdateEnabled)
            {
                if (wait)
                    WaitForTask();

                UpdateCurrentMapAsync();
            }
        }

        private void TimeMachine_LatitudeChanged(object? sender, Utilities.DataChangedEventArgs<double> e)
        {
            OnAnyPropertyChanged();
        }

        private void TimeMachine_LongitudeChanged(object? sender, Utilities.DataChangedEventArgs<double> e)
        {
            OnAnyPropertyChanged();
        }

        private void TimeMachine_DateChanged(object? sender, Utilities.DataChangedEventArgs<DateTime> e)
        {
            OnAnyPropertyChanged();
        }

        private void TimeMachine_SetCurrentDateChanged(object? sender, DataChangedEventArgs<DateTime> e)
        {
            OnAnyPropertyChanged(true);
        }

        public async void UpdateCurrentMapAsync()
        {
            if (updateCurrentMapTask != null && updateCurrentMapTask.Status == TaskStatus.Running)
                CancelTask();

            tokenSource = new CancellationTokenSource();
            var ct = tokenSource.Token;

            updateCurrentMapTask = Task.Run(() =>
            {
                UpdateCurrentMap(ct);
            }, tokenSource.Token);

            await updateCurrentMapTask;
        }

        public void UpdateCurrentMap(CancellationToken ct = default)
        {
            IsWorking = true;
            lock (lockLoadingMap)
            {
                try
                {
                    ct.ThrowIfCancellationRequested();

                    Filter(() => ct.IsCancellationRequested);
                    ct.ThrowIfCancellationRequested();
                    //celestialObjects.Clear();
                    LoadCurrentMap(() => ct.IsCancellationRequested);
                }
                catch (OperationCanceledException) { }
            }
            IsWorking = false;
        }

        private void Filter(Func<bool>? cancel)
        {
            //keptObjects.Clear();
            swoaDb.ClearBlackList();
            for (int i = 0; i < ((ICollection<CelestialObject>)celestialObjects).Count; i++)
            {
                var obj = celestialObjects.ElementAt(i);

                var (ra, dec) = (obj.EquatorialCoordinates.RightAscension, obj.EquatorialCoordinates.Declination);
                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, dec, TimeMachine.Date.ToUniversalTime(), TimeMachine.Latitude, TimeMachine.Longitude);

                if (alt >= 0)
                {
                    swoaDb.AddBlackListId(obj.Id);
                    obj.HorizonCoordinates = new Astronomy.Units.HorizonCoordinates(alt, az);
                }
                else
                {
                    celestialObjects.Remove(obj);
                    i--;
                }

                if (cancel != null && cancel())
                    return;
            }
        }

        private void LoadCurrentMap(Func<bool>? cancel)
        {
            var str_query = $"notblacklisted(id) AND mag <= 4 AND (90 - {TimeMachine.Latitude} + dec) >= 0 AND skycontains(ra, dec, '{TimeMachine.Date.ToString("dd/MM/yyyy HH:mm:ss")}', {TimeMachine.Latitude}, {TimeMachine.Longitude})";
            var records = swoaDb.GetAllSwoaDbRecords(str_query, cancel);

            foreach (var record in records)
            {
                //if (keptObjects.Contains(record.Id))
                //    continue;

                var ra = record.Ra / 24.0 * 360.0;

                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, record.Dec, TimeMachine.Date.ToUniversalTime(), TimeMachine.Latitude, TimeMachine.Longitude);

                var celestialObj = new OutsideStarObject()
                {
                    Id = record.Id,
                    Name = GetStarObjectName((SwoaDbStarRecord)record),
                    EquatorialCoordinates = new Astronomy.Units.EquatorialCoordinates(record.Dec, ra),
                    HorizonCoordinates = new Astronomy.Units.HorizonCoordinates(alt, az),
                    VisualMagnitude = record.Mag,
                    DistanceToSun = record.SunDist,
                    DistanceToEarth = record.SunDist,
                    SpectralClass = ((SwoaDbStarRecord)record).Spect
                };

                celestialObjects.Add(celestialObj);

                if (cancel != null && cancel())
                    return;
            }
        }

        public void CancelTask()
        {
            tokenSource?.Cancel();
            WaitForTask();
            IsWorking = false;
        }

        public void WaitForTask()
        {
            try
            {
                updateCurrentMapTask?.Wait();
            }
            catch (AggregateException e) when (e.InnerException is TaskCanceledException) { }
        }

        public TaskStatus GetTaskStatus()
        {
            if (updateCurrentMapTask != null)
                return updateCurrentMapTask.Status;
            else
                return TaskStatus.RanToCompletion;
        }

        private string GetStarObjectName(SwoaDbStarRecord swoaDbStarRecord)
        {
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Proper))
                return swoaDbStarRecord.Proper;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Bf))
                return swoaDbStarRecord.Bf;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Gl))
                return swoaDbStarRecord.Gl;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Hr))
                return swoaDbStarRecord.Hr;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Hd))
                return swoaDbStarRecord.Hd;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Hip))
                return swoaDbStarRecord.Hip;

            return "None";
        }

        #endregion

    }
}
