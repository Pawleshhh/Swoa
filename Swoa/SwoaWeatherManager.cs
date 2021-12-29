using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa
{
    public class SwoaWeatherManager
    {

        #region Properties

        public double ObservatoryHeight { get; set; }
        public int Cloudiness { get; set; } = 1;
        public int ArtificialLighting { get; set; } = 1;
        public bool IsMoonVisible { get; set; } = false;

        public int WeatherCondition { get; private set; }

        #endregion

        #region Methods

        public void CheckWeatherCondition()
        {
            WeatherCondition = 0;

            CheckObservatoryHeight().
                CheckRest(Cloudiness).
                CheckRest(ArtificialLighting).
                CheckIsMoonVisible();
        }

        private SwoaWeatherManager CheckObservatoryHeight()
        {
            if (ObservatoryHeight < 0)
                WeatherCondition -= 2;
            else if (ObservatoryHeight < 50)
                WeatherCondition -= 1;
            else if (ObservatoryHeight < 100)
                WeatherCondition = WeatherCondition;
            else if (ObservatoryHeight < 200)
                WeatherCondition += 1;
            else
                WeatherCondition += 2;

            return this;
        }

        private SwoaWeatherManager CheckIsMoonVisible()
        {
            if (IsMoonVisible)
                WeatherCondition -= 2;
            else
                WeatherCondition += 2;

            return this;
        }

        private SwoaWeatherManager CheckRest(int value)
        {
            WeatherCondition += (3 - value);

            return this;
        }

        #endregion

    }
}
