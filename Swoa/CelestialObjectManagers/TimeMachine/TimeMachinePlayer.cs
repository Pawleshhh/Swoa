using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Utilities;
using static Utilities.PropertyChangedHelper;

namespace Swoa
{
    public class TimeMachinePlayer : IDisposable
    {

        #region Constructors

        public TimeMachinePlayer(TimeMachine timeMachine)
        {
            this.timeMachine = timeMachine ?? throw new ArgumentNullException(nameof(timeMachine));

            TimeForward = true;
        }

        #endregion

        #region Fields

        private readonly TimeMachine timeMachine;

        private bool timeForward;
        private bool isPlaying;
        private TimeMachinePlayerSpeed playerSpeed = TimeMachinePlayerSpeed.BySecond;

        private Timer timer = new Timer();

        #endregion

        #region Properties

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

        #endregion

        #region Events

        public event EventHandler<DataChangedEventArgs<bool>>? TimeForwardChanged;
        public event EventHandler<DataChangedEventArgs<bool>>? IsPlayingChanged;
        public event EventHandler<DataChangedEventArgs<TimeMachinePlayerSpeed>>? PlayerSpeedChanged;

        protected void OnTimeForwardChanged(bool prev, bool curr)
            => TimeForwardChanged?.Invoke(this, new DataChangedEventArgs<bool>(prev, curr));
        protected void OnIsPlayingChanged(bool prev, bool curr)
            => IsPlayingChanged?.Invoke(this, new DataChangedEventArgs<bool>(prev, curr));
        protected void OnPlayerSpeedChanged(TimeMachinePlayerSpeed prev, TimeMachinePlayerSpeed curr)
            => PlayerSpeedChanged?.Invoke(this, new DataChangedEventArgs<TimeMachinePlayerSpeed>(prev, curr));

        #endregion

        #region Methods

        public void Start()
        {
            if (IsPlaying)
                return;

            timer = new Timer();
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

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateTimeMachine();
        }

        private void UpdateTimeMachine()
        {
            var timeDirection = GetTimeDirection();
            switch (PlayerSpeed)
            {
                case TimeMachinePlayerSpeed.BySecond:
                    timeMachine.Date = timeMachine.Date.AddSeconds(timeDirection); break;
                case TimeMachinePlayerSpeed.ByMinute:
                    timeMachine.Date = timeMachine.Date.AddMinutes(timeDirection); break;
                case TimeMachinePlayerSpeed.ByHour:
                    timeMachine.Date = timeMachine.Date.AddHours(timeDirection); break;
                case TimeMachinePlayerSpeed.ByDay:
                    timeMachine.Date = timeMachine.Date.AddDays(timeDirection); break;
                case TimeMachinePlayerSpeed.ByMonth:
                    timeMachine.Date = timeMachine.Date.AddMonths(timeDirection); break;
                case TimeMachinePlayerSpeed.ByYear:
                    timeMachine.Date = timeMachine.Date.AddYears(timeDirection); break;
            }
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
        }

        private int GetTimeDirection()
            => TimeForward ? 1 : -1;

        #endregion

    }
}
