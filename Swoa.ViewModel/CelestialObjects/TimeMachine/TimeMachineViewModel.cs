using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Swoa.ViewModel
{
    public class TimeMachineViewModel : NotifyPropertyChanges, IAsyncTaskDirector
    {

        #region Constructors

        public TimeMachineViewModel(TimeMachine timeMachine)
        {
            this.timeMachine = timeMachine ?? throw new ArgumentNullException(nameof(timeMachine));

            timeMachine.DateChanged += TimeMachine_DateChanged;
            timeMachine.LatitudeChanged += TimeMachine_LatitudeChanged;
            timeMachine.LongitudeChanged += TimeMachine_LongitudeChanged;
            timeMachine.IsWorkingChanged += TimeMachine_IsWorkingChanged;
        }

        #endregion

        #region Fields

        private readonly TimeMachine timeMachine;

        private TimeSpan time;

        #endregion

        #region Properties

        public bool IsWorking => timeMachine.IsWorking;

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
            get => time;
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
        }

        private void TimeMachine_IsWorkingChanged(object sender, DataChangedEventArgs<bool> e)
        {
            OnPropertyChanged(nameof(IsWorking));
        }

        public void CancelTask()
        {
            timeMachine.CancelTask();
        }

        public void WaitForTask()
        {
            timeMachine.WaitForTask();
        }

        public TaskStatus GetTaskStatus()
        {
            return timeMachine.GetTaskStatus();
        }

        #endregion

    }
}
