using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Swoa.ViewModel
{
    public class TimeMachinePlayerViewModel : NotifyPropertyChanges
    {

        #region Constructors
        public TimeMachinePlayerViewModel(TimeMachinePlayer timeMachinePlayer)
        {
            this.timeMachinePlayer = timeMachinePlayer ?? throw new ArgumentNullException(nameof(timeMachinePlayer));
            timeMachinePlayer.IsPlayingChanged += TimeMachinePlayer_IsPlayingChanged;
            timeMachinePlayer.PlayerSpeedChanged += TimeMachinePlayer_PlayerSpeedChanged;
        }

        #endregion

        #region Fields

        private readonly TimeMachinePlayer timeMachinePlayer;

        #endregion

        #region Properties

        public bool TimeForward
        {
            get => timeMachinePlayer.TimeForward;
            set => SetProperty(() => timeMachinePlayer.TimeForward == value, () => timeMachinePlayer.TimeForward = value);
        }

        public bool IsPlaying => timeMachinePlayer.IsPlaying;

        public TimeMachinePlayerSpeed PlayerSpeed => timeMachinePlayer.PlayerSpeed;

        #endregion

        #region Methods

        private void TimeMachinePlayer_PlayerSpeedChanged(object sender, Utilities.DataChangedEventArgs<TimeMachinePlayerSpeed> e)
        {
            OnPropertyChanged(nameof(PlayerSpeed));
        }

        private void TimeMachinePlayer_IsPlayingChanged(object sender, Utilities.DataChangedEventArgs<bool> e)
        {
            OnPropertyChanged(nameof(IsPlaying));
        }

        #endregion

        #region Commands

        private ICommand play;
        public ICommand Play => RelayCommand.Create(ref play, o =>
        {
            if (o is bool start)
            {
                if (start)
                    timeMachinePlayer.Start();
                else
                    timeMachinePlayer.Stop();
            }
            else if (o is null)
                timeMachinePlayer.Stop();
        });

        private ICommand speedUp;
        public ICommand SpeedUp => RelayCommand.Create(ref speedUp, _ => timeMachinePlayer.SpeedUp());

        private ICommand slowDown;
        public ICommand SlowDown => RelayCommand.Create(ref slowDown, _ => timeMachinePlayer.SlowDown());

        private ICommand setTimeBackward;
        public ICommand SetTimeBackward => RelayCommand.Create(ref setTimeBackward, _ => TimeForward = false);

        private ICommand setTimeForward;
        public ICommand SetTimeForward => RelayCommand.Create(ref setTimeForward, _ => TimeForward = true);

        #endregion

    }
}
