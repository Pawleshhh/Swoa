using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public class MeasureableObjectBuilder : CelestialObjectBuilderDecorator
    {
        public MeasureableObjectBuilder(ICelestialObjectBuilder celestialObjectBuilder) : base(celestialObjectBuilder)
        {
        }

        public double ApparentDiameter { get; set; }
        public double Radius { get; set; }
        public double Volume { get; set; }
        public double Mass { get; set; }
        public double Density { get; set; }
        public double SurfaceArea { get; set; }
        public double Gravity { get; set; }
        public double AverageTemperature { get; set; }

        protected override Dictionary<string, IProperty> GetDecoratorProperties()
        {
            var properties = new Dictionary<string, IProperty>();
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(ApparentDiameter), ApparentDiameter));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Radius), Radius));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Volume), Volume));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Mass), Mass));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Density), Density));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(SurfaceArea), SurfaceArea));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Gravity), Gravity));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(AverageTemperature), AverageTemperature));

            return properties;
        }
    }
}
