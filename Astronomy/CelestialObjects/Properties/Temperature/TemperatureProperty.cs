using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects.Properties
{
    public class TemperatureProperty : PropertyBase<double>
    {

        public TemperatureProperty(string name, double temperature, TemperatureUnit unit)
            : base(name, temperature)
        {
            if (!Enum.IsDefined(typeof(TemperatureUnit), unit))
                throw new ArgumentException(nameof(unit));

            TemperatureUnit = unit;
        }

        public TemperatureUnit TemperatureUnit { get; }

    }
}
