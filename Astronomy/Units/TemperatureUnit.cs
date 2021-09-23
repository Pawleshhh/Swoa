using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.Units
{
    public sealed class TemperatureUnit : Unit
    {

        private TemperatureUnit(string unitName, string unitSignature)
            : base(unitName, unitSignature)
        {

        }

        public static TemperatureUnit Kelvin { get; } = new TemperatureUnit("Kelvin", "K");
        public static TemperatureUnit Celsius { get; } = new TemperatureUnit("Celsius", "°C");
        public static TemperatureUnit Fahrenheit { get; } = new TemperatureUnit("Fahrenheit", "F");

    }
}
