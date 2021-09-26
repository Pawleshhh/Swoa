using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public class DistancedFromEarthObjectBuilder : CelestialObjectBuilderDecorator
    {
        public DistancedFromEarthObjectBuilder(ICelestialObjectBuilder celestialObjectBuilder) : base(celestialObjectBuilder)
        {
        }

        public double DistanceFromEarth { get; set; }
        public double AverageDistanceFromEarth { get; set; }

        protected override Dictionary<string, IProperty> GetDecoratorProperties()
        {
            var properties = new Dictionary<string, IProperty>();
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(DistanceFromEarth), DistanceFromEarth));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(AverageDistanceFromEarth), AverageDistanceFromEarth));
            return properties;
        }
    }
}
