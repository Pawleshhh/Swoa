using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class SwoaWeatherManagerViewModel : NotifyPropertyChanges
    {

        #region Constructors

        public SwoaWeatherManagerViewModel(SwoaWeatherManager weatherManager)
        {
            this.weatherManager = weatherManager ?? throw new ArgumentNullException(nameof(weatherManager));
            this.weatherManager.CheckWeatherCondition();
        }

        #endregion

        #region Fields

        private readonly SwoaWeatherManager weatherManager;

        #endregion

        #region Properties

        public ObservableCollection<string> CloudinessLevels { get; } = new ObservableCollection<string>(SwoaWeatherManager.CloudinessLevels);
        public ObservableCollection<string> ArtificialLightingLevels { get; } = new ObservableCollection<string>(SwoaWeatherManager.ArtificialLightingLevels);

        public string CloudinessLevelSelected
        {
            set
            {
                Cloudiness = CloudinessLevels.IndexOf(value) + 1;
            }
        }

        public string ArtificialLightingSelected
        {
            set
            {
                ArtificialLighting = ArtificialLightingLevels.IndexOf(value) + 1;
            }
        }

        public double ObservatoryHeight
        {
            get => weatherManager.ObservatoryHeight;
            set => SetPropertyHelper(() => weatherManager.ObservatoryHeight == value, () => weatherManager.ObservatoryHeight = value);
        }
        public int Cloudiness
        {
            get => weatherManager.Cloudiness;
            set => SetPropertyHelper(() => weatherManager.Cloudiness == value, () => weatherManager.Cloudiness = value);
        }
        public int ArtificialLighting
        {
            get => weatherManager.ArtificialLighting;
            set => SetPropertyHelper(() => weatherManager.ArtificialLighting == value, () => weatherManager.ArtificialLighting = value);
        }
        public bool IsMoonVisible
        {
            get => weatherManager.IsMoonVisible;
            set => SetPropertyHelper(() => weatherManager.IsMoonVisible == value, () => weatherManager.IsMoonVisible = value);
        }

        public int WeatherCondition => weatherManager.WeatherCondition;

        private bool isPanelShown;
        public bool IsPanelShown
        {
            get => isPanelShown;
            set => SetProperty(ref isPanelShown, value);
        }

        #endregion

        #region Method


        private void SetPropertyHelper(Func<bool> equal, Action set, [CallerMemberName] string propertyName = null)
        {
            if (SetProperty(equal, set, propertyName))
            {
                weatherManager.CheckWeatherCondition();
                OnPropertyChanged(nameof(WeatherCondition));
            }
        }

        #endregion

    }
}
