using Astronomy;
using Astronomy.Units;
using CelestialObjects;
using SwoaDatabaseAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using static Utilities.PropertyChangedHelper;

namespace Swoa
{
    public class CelestialObjectsLoader : IAsyncTaskDirector
    {

        #region Constructors
        public CelestialObjectsLoader(SwoaDb swoaDb, TimeMachine timeMachine, ICelestialObjectCollection celestialObjects)
        {
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));
            this.timeMachine = timeMachine ?? throw new ArgumentNullException(nameof(timeMachine));
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
        }
        #endregion

        #region Fields

        private readonly SwoaDb swoaDb;
        private readonly TimeMachine timeMachine;
        private readonly ICelestialObjectCollection celestialObjects;

        private bool isWorking;

        private CancellationTokenSource? tokenSource;
        private Task? updateCurrentMapTask;
        private object lockLoadingMap = new object();

        #endregion

        #region Properties

        public bool IsWorking
        {
            get => isWorking;
            set => SetProperty(ref isWorking, value, OnIsWorkingChanged);
        }    

        #endregion

        #region Events

        public event EventHandler<DataChangedEventArgs<bool>>? IsWorkingChanged;

        protected void OnIsWorkingChanged(bool prev, bool curr)
            => IsWorkingChanged?.Invoke(this, new DataChangedEventArgs<bool>(prev, curr));

        #endregion

        #region Methods

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

                    Filter(ct);

                    ct.ThrowIfCancellationRequested();

                    LoadCurrentMap(ct);
                }
                catch (OperationCanceledException) { }
            }
            IsWorking = false;
        }

        private void Filter(CancellationToken ct = default)
        {
            //keptObjects.Clear();
            swoaDb.ClearBlackList();
            for (int i = 0; i < ((ICollection<CelestialObject>)celestialObjects).Count; i++)
            {
                var obj = celestialObjects.ElementAt(i);

                var (ra, dec) = (obj.EquatorialCoordinates.RightAscension, obj.EquatorialCoordinates.Declination);
                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, dec, timeMachine.Date.ToUniversalTime(), timeMachine.Latitude, timeMachine.Longitude);

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

                ct.ThrowIfCancellationRequested();
            }
        }

        private void LoadCurrentMap(CancellationToken ct = default)
        {
            var str_query = $"notblacklisted(id) AND mag <= 3 " +
                $"AND (90 - {timeMachine.Latitude} + dec) >= 0 " +
                $"AND skycontains(ra, dec, '{timeMachine.Date.ToString("dd/MM/yyyy HH:mm:ss")}', {timeMachine.Latitude}, {timeMachine.Longitude})";
            var records = swoaDb.GetAllSwoaDbRecords(str_query, ct);

            foreach (var record in records)
            {
                //if (keptObjects.Contains(record.Id))
                //    continue;

                var ra = record.Ra / 24.0 * 360.0;

                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, record.Dec, timeMachine.Date.ToUniversalTime(), timeMachine.Latitude, timeMachine.Longitude);

                var celestialObj = CelestialObjectFactory.CreateFromSwoaDbRecord(record, new EquatorialCoordinates(record.Dec, ra), new HorizonCoordinates(alt, az));

                celestialObjects.Add(celestialObj);

                ct.ThrowIfCancellationRequested();
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

        #endregion

    }
}
