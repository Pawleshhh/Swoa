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

        public TimeMachine(SwoaDb swoaDb)
        {
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));
        }

        #endregion

        #region Fields

        private readonly SwoaDb swoaDb;

        private double latitude;
        private double longitude;

        private DateTime date;

        private bool isWorking;

        private CancellationTokenSource tokenSource;

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

        public IEnumerable<CelestialObject> GetCurrentMap()
        {
            return GetCurrentMap(null);
        }

        public async Task<IEnumerable<CelestialObject>> GetCurrentMapAsync()
        {
            if (!IsWorking)
            {
                IsWorking = true;
            }

            tokenSource = new CancellationTokenSource();
            var ct = tokenSource.Token;

            try
            {
                return await Task.Run(() =>
                {
                    ct.ThrowIfCancellationRequested();

                    return GetCurrentMap(() => ct.IsCancellationRequested);
                }, tokenSource.Token);
            }
            finally
            {
                IsWorking = false;
            }
        }

        public void CancelWork()
        {
            tokenSource.Cancel();
            IsWorking = false;
        }

        private IEnumerable<CelestialObject> GetCurrentMap(Func<bool>? cancel)
        {
            var records = swoaDb.GetAllSwoaDbRecords("mag <= 5", cancel);

            foreach (var record in records)
            {
                var ra = record.Ra / 24.0 * 360.0;

                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, record.Dec, Date.ToUniversalTime(), latitude, longitude);

                if (alt <= 0)
                    continue;

                var celestialObj = new OutsideStarObject()
                {
                    EquatorialCoordinates = new Astronomy.Units.EquatorialCoordinates(record.Dec, record.Ra),
                    HorizontalCoordinates = new Astronomy.Units.HorizonCoordinates(alt, az),
                    VisualMagnitude = record.Mag,
                    SpectralClass = ((SwoaDbStarRecord)record).Spect
                };

                yield return celestialObj;

                if (cancel != null && cancel())
                    yield break;
            }
        }

        #endregion

    }
}
