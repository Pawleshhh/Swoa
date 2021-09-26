using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public class DistancedFromSunObjectBuilder : CelestialObjectBuilderDecorator
    {
        public DistancedFromSunObjectBuilder(ICelestialObjectBuilder celestialObjectBuilder) : base(celestialObjectBuilder)
        {
        }

        public double DistanceFromSun { get; set; }
        public double AverageDistanceFromSun { get; set; }

        protected override Dictionary<string, IProperty> GetDecoratorProperties()
        {
            var properties = new Dictionary<string, IProperty>();
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(DistanceFromSun), DistanceFromSun));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(AverageDistanceFromSun), AverageDistanceFromSun));

            return properties;
        }
    }
}
