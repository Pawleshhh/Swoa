using Astronomy.CelestialObjects.Properties;
using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public abstract class CelestialObjectBuilder
    {

        public string Name { get; set; } = string.Empty;

        public double VisualMagnitude { get; set; }

        public HorizonCoordinates HorizonCoordinates { get; set; }
        public EquatorialCoordinates EquatorialCoordinates { get; set; }

        public virtual CelestialObjectBase BuildCelestialObject()
        {
            var properties = GetProperties();
            return new CelestialObjectBase(Name, VisualMagnitude, HorizonCoordinates, EquatorialCoordinates, properties);
        }

        protected abstract Dictionary<string, IProperty> GetProperties();

    }
}
