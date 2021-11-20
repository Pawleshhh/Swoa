using Astronomy;
using CelestialObjects;
using SwoaDatabaseAPI;
using System;
using timers = System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using static Utilities.PropertyChangedHelper;

namespace Swoa
{
    public class TimeMachine
    {
        #region Constructors

        public TimeMachine(IDateTimeService dateTimeService)
        {
            this.dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));

            Date = dateTimeService.GetLocalDateTime();
            TimeForward = true;
        }

        #endregion Constructors

        #region Fields

        private readonly IDateTimeService dateTimeService;

        private double magnitude = 5;
        private double latitude;
        private double longitude;
        private DateTime date;
        private bool timeForward;
        private bool isPlaying;
        private TimeMachinePlayerSpeed playerSpeed = TimeMachinePlayerSpeed.BySecond;

        private timers.Timer timer = new timers.Timer();

        #endregion Fields

        #region Properties

        public double Magnitude
        {
            get => magnitude;
            set => SetProperty(ref magnitude, value, OnMagnitudeChanged);
        }

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

        public bool TimeForward
        {
            get => timeForward;
            set => SetProperty(ref timeForward, value, OnTimeForwardChanged);
        }

        public bool IsPlaying
        {
            get => isPlaying;
            private set => SetProperty(ref isPlaying, value, OnIsPlayingChanged);
        }

        public TimeMachinePlayerSpeed PlayerSpeed
        {
            get => playerSpeed;
            private set => SetProperty(ref playerSpeed, value, OnPlayerSpeedChanged);
        }

        #endregion Properties

        #region Events

        public event EventHandler<DataChangedEventArgs<DateTime>>? DateChanged;
        public event EventHandler<DataChangedEventArgs<DateTime>>? SetCurrentDateChanged;
        public event EventHandler<DataChangedEventArgs<double>>? MagnitudeChanged;
        public event EventHandler<DataChangedEventArgs<double>>? LatitudeChanged;
        public event EventHandler<DataChangedEventArgs<double>>? LongitudeChanged;
        public event EventHandler<DataChangedEventArgs<bool>>? TimeForwardChanged;
        public event EventHandler<DataChangedEventArgs<bool>>? IsPlayingChanged;
        public event EventHandler<DataChangedEventArgs<TimeMachinePlayerSpeed>>? PlayerSpeedChanged;

        protected void OnDateChanged(DateTime previous, DateTime current)
            => DateChanged?.Invoke(this, new DataChangedEventArgs<DateTime>(previous, current));
        protected void OnSetCurrentDateChanged(DateTime previous, DateTime current)
            => SetCurrentDateChanged?.Invoke(this, new DataChangedEventArgs<DateTime>(previous, current));

        protected void OnLatitudeChanged(double previous, double current)
            => LatitudeChanged?.Invoke(this, new DataChangedEventArgs<double>(previous, current));

        protected void OnLongitudeChanged(double previous, double current)
            => LongitudeChanged?.Invoke(this, new DataChangedEventArgs<double>(previous, current));

        protected void OnMagnitudeChanged(double previous, double current)
            => MagnitudeChanged?.Invoke(this, new DataChangedEventArgs<double>(previous, current));

        protected void OnTimeForwardChanged(bool prev, bool curr)
           => TimeForwardChanged?.Invoke(this, new DataChangedEventArgs<bool>(prev, curr));
        protected void OnIsPlayingChanged(bool prev, bool curr)
            => IsPlayingChanged?.Invoke(this, new DataChangedEventArgs<bool>(prev, curr));
        protected void OnPlayerSpeedChanged(TimeMachinePlayerSpeed prev, TimeMachinePlayerSpeed curr)
            => PlayerSpeedChanged?.Invoke(this, new DataChangedEventArgs<TimeMachinePlayerSpeed>(prev, curr));

        #endregion Events

        #region Methods

        public void Start()
        {
            if (IsPlaying)
                return;

            timer = new timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;

            IsPlaying = true;

            timer.Start();
        }

        public void Stop()
        {
            if (!IsPlaying)
                return;

            timer.Stop();
            timer.Dispose();

            IsPlaying = false;
        }

        public void SpeedUp()
        {
            if (PlayerSpeed == TimeMachinePlayerSpeed.ByYear)
            {
                PlayerSpeed = TimeMachinePlayerSpeed.BySecond;
            }
            else
            {
                PlayerSpeed++;
            }
        }

        public void SlowDown()
        {
            if (PlayerSpeed == TimeMachinePlayerSpeed.BySecond)
            {
                PlayerSpeed = TimeMachinePlayerSpeed.ByYear;
            }
            else
            {
                PlayerSpeed--;
            }
        }

        public void SetCurrentDate()
        {
            if (IsPlaying)
                Stop();

            Date = dateTimeService.GetLocalDateTime();
        }

        private void Timer_Elapsed(object sender, timers.ElapsedEventArgs e)
        {
            UpdateTimeMachine();
        }

        private void UpdateTimeMachine()
        {
            var timeDirection = GetTimeDirection();
            switch (PlayerSpeed)
            {
                case TimeMachinePlayerSpeed.BySecond:
                    Date = Date.AddSeconds(timeDirection); break;
                case TimeMachinePlayerSpeed.ByMinute:
                    Date = Date.AddMinutes(timeDirection); break;
                case TimeMachinePlayerSpeed.ByHour:
                    Date = Date.AddHours(timeDirection); break;
                case TimeMachinePlayerSpeed.ByDay:
                    Date = Date.AddDays(timeDirection); break;
                case TimeMachinePlayerSpeed.ByMonth:
                    Date = Date.AddMonths(timeDirection); break;
                case TimeMachinePlayerSpeed.ByYear:
                    Date = Date.AddYears(timeDirection); break;
            }
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
        }

        private int GetTimeDirection()
            => TimeForward ? 1 : -1;


        #endregion Methods
    }
}