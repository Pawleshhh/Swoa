using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public class RotatingObjectBuilder : CelestialObjectBuilderDecorator
    {
        public RotatingObjectBuilder(ICelestialObjectBuilder celestialObjectBuilder) : base(celestialObjectBuilder)
        {
        }

        public TimeSpan LengthOfDay { get; set; }

        protected override Dictionary<string, IProperty> GetDecoratorProperties()
        {
            var properties = new Dictionary<string, IProperty>();
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(LengthOfDay), LengthOfDay));

            return properties;
        }
    }
}
