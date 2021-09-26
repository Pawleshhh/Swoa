using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public class ConstellationMemberObjectBuilder : CelestialObjectBuilderDecorator
    {
        public ConstellationMemberObjectBuilder(ICelestialObjectBuilder celestialObjectBuilder) : base(celestialObjectBuilder)
        {
        }

        public string Constellation { get; set; } = string.Empty;

        protected override Dictionary<string, IProperty> GetDecoratorProperties()
        {
            var properties = new Dictionary<string, IProperty>();
            properties.AddKeyValuePair(CelestialObjectBuilder.GetKeyValuePairNormalized(nameof(Constellation), Constellation));
            return properties;
        }
    }
}
