using Astronomy;
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
    public class TimeMachine
    {

        #region Constructors

        public TimeMachine(SwoaDb swoaDb, ICelestialObjectCollection celestialObjects)
        {
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
        }

        #endregion

        #region Fields

        private readonly SwoaDb swoaDb;
        private readonly ICelestialObjectCollection celestialObjects;

        private readonly HashSet<long> keptObjects = new HashSet<long>();

        private double latitude;
        private double longitude;

        private DateTime date;

        private bool isWorking;

        private CancellationTokenSource? tokenSource;

        private object _filterLocker = new object();

        #endregion

        #region Properties

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

        #endregion

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


        #endregion

        #region Methods

        //public void UpdateCurrentMap()
        //{
        //    LoadCurrentMap(null);
        //}

        public async void UpdateCurrentMapAsync()
        {
            if (IsWorking)
                CancelWork();

            IsWorking = true;

            tokenSource = new CancellationTokenSource();
            var ct = tokenSource.Token;

            try
            {
                await Task.Run(() =>
                {
                    ct.ThrowIfCancellationRequested();

                    Filter(() => ct.IsCancellationRequested);
                    ct.ThrowIfCancellationRequested();
                    //celestialObjects.Clear();
                    LoadCurrentMap(() => ct.IsCancellationRequested);
                }, tokenSource.Token);
            }
            catch(OperationCanceledException) { }
            finally
            {
                IsWorking = false;
            }
        }

        public void CancelWork()
        {
            tokenSource?.Cancel();
            IsWorking = false;
        }

        private void Filter(Func<bool>? cancel)
        {
            lock (_filterLocker)
            {
                keptObjects.Clear();
                for (int i = 0; i < ((ICollection<CelestialObject>)celestialObjects).Count; i++)
                {
                    var obj = celestialObjects.ElementAt(i);
                    if (obj == null) continue;
                    var (ra, dec) = (obj.EquatorialCoordinates.RightAscension, obj.EquatorialCoordinates.Declination);
                    var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, dec, Date.ToUniversalTime(), latitude, longitude);

                    if (alt >= 0)
                    {
                        keptObjects.Add(obj.Id);
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
        }

        private void LoadCurrentMap(Func<bool>? cancel)
        {
            var str_query = $"mag <= 6 AND (90 - {Latitude} + dec) >= 0 AND skycontains(ra, dec, '{Date.ToString("dd/MM/yyyy HH:mm:ss")}', {Latitude}, {Longitude})";
            var records = swoaDb.GetAllSwoaDbRecords(str_query, cancel);

            foreach (var record in records)
            {
                if (keptObjects.Contains(record.Id))
                    continue;

                var ra = record.Ra / 24.0 * 360.0;

                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, record.Dec, Date.ToUniversalTime(), latitude, longitude);

                var celestialObj = new OutsideStarObject()
                {
                    Id = record.Id,
                    EquatorialCoordinates = new Astronomy.Units.EquatorialCoordinates(record.Dec, ra),
                    HorizontalCoordinates = new Astronomy.Units.HorizonCoordinates(alt, az),
                    VisualMagnitude = record.Mag,
                    SpectralClass = ((SwoaDbStarRecord)record).Spect
                };

                celestialObjects.Add(celestialObj);

                if (cancel != null && cancel())
                    return;
            }
        }

        #endregion

    }
}
