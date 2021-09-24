using Astronomy.CelestialObjects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class DeepSkyObjectBuilder : CelestialObjectBuilder
    {

        public string DeepSkyObjectType { get; set; } = string.Empty;

        protected override Dictionary<string, IProperty> GetProperties()
        {
            var properties = new Dictionary<string, IProperty>(new KeyValuePair<string, IProperty>[]
            {
                AllProperties.DeepSkyObjectType.ToKeyValuePair(DeepSkyObjectType)
            });

            return properties;
        }
    }
}
