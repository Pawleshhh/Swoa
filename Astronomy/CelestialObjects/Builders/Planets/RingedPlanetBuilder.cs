using Astronomy.CelestialObjects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Astronomy.CelestialObjects
{
    public class RingedPlanetBuilder : PlanetObjectBuilder
    {

        public int RingCount { get; set; }

        protected override Dictionary<string, IProperty> GetProperties()
        {
            var properties = base.GetProperties();
            properties.AddKeyValuePair(AllProperties.RingCount.ToKeyValuePair(RingCount));

            return properties;
        }

    }
}
