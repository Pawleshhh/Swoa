using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class MoonObjectType : CelestialObjectType
    {
        protected MoonObjectType(string name) : base(name)
        {
        }

        public static MoonObjectType DefaultMoon { get; } = new MoonObjectType("Default moon");

        public override CelestialObjectType GetDefault()
        {
            return DefaultMoon;
        }

    }
}
