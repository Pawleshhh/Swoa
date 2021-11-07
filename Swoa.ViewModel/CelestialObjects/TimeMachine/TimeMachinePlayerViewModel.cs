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

        #endregion

    }
}
