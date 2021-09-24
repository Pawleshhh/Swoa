using Astronomy.CelestialObjects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class StarObjectBuilder : CelestialObjectBuilder
    {

        public double AbsoluteMagnitude { get; set; }

        public string SpectralClass { get; set; } = string.Empty;

        public double DistanceToSun { get; set; }

        protected override Dictionary<string, IProperty> GetProperties()
        {
            var properties = new Dictionary<string, IProperty>(new KeyValuePair<string, IProperty>[]
            {
                AllProperties.AbsoluteMagnitude.ToKeyValuePair(AbsoluteMagnitude),
                AllProperties.SpectralClass.ToKeyValuePair(SpectralClass),
                AllProperties.DistanceToSun.ToKeyValuePair(DistanceToSun)
            });

            return properties;
        }
    }
}
