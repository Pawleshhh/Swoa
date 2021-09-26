using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public class OrbitingObjectBuilder : RotatingObjectBuilder
    {
        public OrbitingObjectBuilder(ICelestialObjectBuilder celestialObjectBuilder) : base(celestialObjectBuilder)
        {
        }

        public TimeSpan LengthOfYear { get; set; }
        public double OrbitalVelocity { get; set; }
        public double Eccentricity { get; set; }
        public double SemiMajorAxis { get; set; }
        public double Inclination { get; set; }
        public double Obliquity { get; set; }

        protected override Dictionary<string, IProperty> GetDecoratorProperties()
        {
            var properties = base.GetDecoratorProperties();
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(LengthOfYear), LengthOfYear));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(OrbitalVelocity), OrbitalVelocity));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Eccentricity), Eccentricity));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(SemiMajorAxis), SemiMajorAxis));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Inclination), Inclination));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Obliquity), Obliquity));

            return properties;
        }

    }
}
