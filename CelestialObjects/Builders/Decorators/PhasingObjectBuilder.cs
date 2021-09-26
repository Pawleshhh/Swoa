using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public class PhasingObjectBuilder : CelestialObjectBuilderDecorator
    {
        public PhasingObjectBuilder(ICelestialObjectBuilder celestialObjectBuilder) : base(celestialObjectBuilder)
        {
        }

        public string Phase { get; set; } = string.Empty;
        public double PhaseIllumination { get; set; }

        protected override Dictionary<string, IProperty> GetDecoratorProperties()
        {
            var properties = new Dictionary<string, IProperty>();
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Phase), Phase));
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(PhaseIllumination), PhaseIllumination));

            return properties;
        }
    }
}
