using Astronomy.CelestialObjects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Astronomy.CelestialObjects
{
    public class PhasedPlanetBuilder : PlanetObjectBuilder
    {

        public string Phase { get; set; } = string.Empty;
        public double PhaseIllumination { get; set; }

        protected override Dictionary<string, IProperty> GetProperties()
        {
            var properties = base.GetProperties();
            properties.AddKeyValuePair(AllProperties.Phase.ToKeyValuePair(Phase));
            properties.AddKeyValuePair(AllProperties.PhaseIllumination.ToKeyValuePair(PhaseIllumination));

            return properties;
        }

    }
}
