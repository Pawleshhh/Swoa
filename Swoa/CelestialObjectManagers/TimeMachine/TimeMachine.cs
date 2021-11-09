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
    public class TimeMachine : IAsyncTaskDirector
    {
        #region Constructors

        public TimeMachine(SwoaDb swoaDb, ICelestialObjectCollection celestialObjects, IDateTimeService dateTimeService)
        {
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
            this.dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));

            TimeMachinePlayer = new TimeMachinePlayer(this);
            Date = dateTimeService.GetLocalDateTime();
        }

        #endregion Constructors

        #region Fields

        private readonly SwoaDb swoaDb;
        private readonly ICelestialObjectCollection celestialObjects;
        private readonly IDateTimeService dateTimeService;

        private double latitude;
        private double longitude;

        private DateTime date;

        private bool isWorking;

        private CancellationTokenSource? tokenSource;
        private Task? updateCurrentMapTask;
        private object lockLoadingMap = new object();


        #endregion Fields

        #region Properties

        public TimeMachinePlayer TimeMachinePlayer { get; }

        public double Latitude
        {
            get => latitude;
            set => SetProperty(ref latitude, value, OnLatitudeChanged);
        }

        public double Longitude
        {
            get => longitude;
            set => SetProperty(ref longitude, value, OnLongitudeChanged);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value, OnDateChanged);
        }

        public bool IsWorking
        {
            get => isWorking;
            set => SetProperty(ref isWorking, value, OnIsWorkingChanged);
        }

        #endregion Properties

        #region Events

        public event EventHandler<DataChangedEventArgs<DateTime>>? DateChanged;

        public event EventHandler<DataChangedEventArgs<double>>? LatitudeChanged;

        public event EventHandler<DataChangedEventArgs<double>>? LongitudeChanged;

        public event EventHandler<DataChangedEventArgs<bool>>? IsWorkingChanged;

        protected void OnDateChanged(DateTime previous, DateTime current)
            => DateChanged?.Invoke(this, new DataChangedEventArgs<DateTime>(previous, current));

        protected void OnLatitudeChanged(double previous, double current)
            => LatitudeChanged?.Invoke(this, new DataChangedEventArgs<double>(previous, current));

        protected void OnLongitudeChanged(double previous, double current)
            => LongitudeChanged?.Invoke(this, new DataChangedEventArgs<double>(previous, current));

        protected void OnIsWorkingChanged(bool previous, bool current)
            => IsWorkingChanged?.Invoke(this, new DataChangedEventArgs<bool>(previous, current));

        #endregion Events

        #region Methods

        //public void UpdateCurrentMap()
        //{
        //    LoadCurrentMap(null);
        //}

        public void SetCurrentDate()
        {
            if (TimeMachinePlayer.IsPlaying)
                TimeMachinePlayer.Stop();

            WaitForTask();

            Date = dateTimeService.GetLocalDateTime();
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
                catch (OperationCanceledException) {  }
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
                //if (obj == null) continue;
                var (ra, dec) = (obj.EquatorialCoordinates.RightAscension, obj.EquatorialCoordinates.Declination);
                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, dec, Date.ToUniversalTime(), latitude, longitude);

                if (alt >= 0)
                {
                    swoaDb.AddBlackListId(obj.Id);
                    obj.HorizontalCoordinates = new Astronomy.Units.HorizonCoordinates(alt, az);
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
            var str_query = $"notblacklisted(id) AND mag <= 4 AND (90 - {Latitude} + dec) >= 0 AND skycontains(ra, dec, '{Date.ToString("dd/MM/yyyy HH:mm:ss")}', {Latitude}, {Longitude})";
            var records = swoaDb.GetAllSwoaDbRecords(str_query, cancel);

            foreach (var record in records)
            {
                //if (keptObjects.Contains(record.Id))
                //    continue;

                var ra = record.Ra / 24.0 * 360.0;

                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, record.Dec, Date.ToUniversalTime(), latitude, longitude);

                var celestialObj = new OutsideStarObject()
                {
                    Id = record.Id,
                    Name = GetStarObjectName((SwoaDbStarRecord)record),
                    EquatorialCoordinates = new Astronomy.Units.EquatorialCoordinates(record.Dec, ra),
                    HorizontalCoordinates = new Astronomy.Units.HorizonCoordinates(alt, az),
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

        #endregion Methods
    }
}