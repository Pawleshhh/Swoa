using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utilities;

namespace Swoa.ViewModel
{
    public class TimeMachineViewModel : NotifyPropertyChanges
    {

        #region Constructors

        public TimeMachineViewModel(TimeMachine timeMachine)
        {
            this.timeMachine = timeMachine ?? throw new ArgumentNullException(nameof(timeMachine));

            timeMachine.DateChanged += TimeMachine_DateChanged;
            timeMachine.LatitudeChanged += TimeMachine_LatitudeChanged;
            timeMachine.LongitudeChanged += TimeMachine_LongitudeChanged;
            timeMachine.IsPlayingChanged += TimeMachinePlayer_IsPlayingChanged;
            timeMachine.PlayerSpeedChanged += TimeMachinePlayer_PlayerSpeedChanged;
            timeMachine.TimeForwardChanged += TimeMachinePlayer_TimeForwardChanged;

        }

        #endregion

        #region Fields

        private readonly TimeMachine timeMachine;

        private TimeSpan time;

        #endregion

        #region Properties

        public double Latitude
        {
            get => timeMachine.Latitude;
            set => SetProperty(() => timeMachine.Latitude == value, () => timeMachine.Latitude = value);
        }

        public double Longitude
        {
            get => timeMachine.Longitude;
            set => SetProperty(() => timeMachine.Longitude == value, () => timeMachine.Longitude = value);
        }

        public DateTime Date
        {
            get => timeMachine.Date;
            set => SetProperty(() => timeMachine.Date == value, () => timeMachine.Date = value);
        }

        public TimeSpan Time
        {
            get => Date.TimeOfDay;
            set
            {
                if (SetProperty(ref time, value))
                {
                    Date = new DateTime(Date.Year,
                        Date.Month,
                        Date.Day,
                        value.Hours,
                        value.Minutes,
                        value.Seconds);
                }
            }
        }

        public bool TimeForward
        {
            get => timeMachine.TimeForward;
            set => SetProperty(() => timeMachine.TimeForward == value, () => timeMachine.TimeForward = value);
        }

        public bool IsPlaying => timeMachine.IsPlaying;

        public TimeMachinePlayerSpeed PlayerSpeed => timeMachine.PlayerSpeed;

        #endregion

        #region Methods

        private void TimeMachine_LatitudeChanged(object sender, DataChangedEventArgs<double> e)
        {
            Latitude = e.Current;
        }

        private void TimeMachine_LongitudeChanged(object sender, DataChangedEventArgs<double> e)
        {
            Longitude = e.Current;
        }

        private void TimeMachine_DateChanged(object sender, DataChangedEventArgs<DateTime> e)
        {
            Date = e.Current;
            Time = new TimeSpan(e.Current.Hour, e.Current.Minute, e.Current.Second);
            OnPropertyChanged(nameof(Time), nameof(Date));
        }

        private void TimeMachinePlayer_PlayerSpeedChanged(object sender, Utilities.DataChangedEventArgs<TimeMachinePlayerSpeed> e)
        {
            OnPropertyChanged(nameof(PlayerSpeed));
        }

        private void TimeMachinePlayer_IsPlayingChanged(object sender, Utilities.DataChangedEventArgs<bool> e)
        {
            OnPropertyChanged(nameof(IsPlaying));
        }

        private void TimeMachinePlayer_TimeForwardChanged(object sender, Utilities.DataChangedEventArgs<bool> e)
        {
            OnPropertyChanged(nameof(TimeForward));
        }

        #endregion

        #region Commands

        private ICommand setCurrentDate;
        public ICommand SetCurrentDate => RelayCommand.Create(ref setCurrentDate, _ => timeMachine.SetCurrentDate());

        private ICommand play;
        public ICommand Play => RelayCommand.Create(ref play, o =>
        {
            if (o is bool start)
            {
                if (start)
                    timeMachine.Start();
                else
                    timeMachine.Stop();
            }
            else if (o is null)
                timeMachine.Stop();
        });

        private ICommand speedUp;
        public ICommand SpeedUp => RelayCommand.Create(ref speedUp, _ => timeMachine.SpeedUp());

        private ICommand slowDown;
        public ICommand SlowDown => RelayCommand.Create(ref slowDown, _ => timeMachine.SlowDown());

        private ICommand setTimeBackward;
        public ICommand SetTimeBackward => RelayCommand.Create(ref setTimeBackward, _ => TimeForward = false);

        private ICommand setTimeForward;
        public ICommand SetTimeForward => RelayCommand.Create(ref setTimeForward, _ => TimeForward = true);


        #endregion

    }
}
