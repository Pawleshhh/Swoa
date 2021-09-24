using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects.CelestialObjectTypes
{
    public class DeepSkyObjectType : CelestialObjectType
    {
        protected DeepSkyObjectType(string name) : base(name)
        {
        }

        public static DeepSkyObjectType DefaultDeepSkyObject { get; } = new DeepSkyObjectType("Default deep sky object");

        public override CelestialObjectType GetDefault()
        {
            return DefaultDeepSkyObject;
        }

    }
}
