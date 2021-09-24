using Astronomy.CelestialObjects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class ConstellationObjectBuilder : CelestialObjectBuilder
    {

        public int StarCount { get; set; }

        protected override Dictionary<string, IProperty> GetProperties()
        {
            var properties = new Dictionary<string, IProperty>(new KeyValuePair<string, IProperty>[]
            {
                AllProperties.StarCount.ToKeyValuePair(StarCount)
            });

            return properties;
        }
    }
}
