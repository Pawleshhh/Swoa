using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class ConstellationType : CelestialObjectType
    {
        protected ConstellationType(string name) : base(name)
        {
        }

        public static ConstellationType DefaultConstellation { get; } = new ConstellationType("Default constellation");

        public override CelestialObjectType GetDefault()
        {
            return DefaultConstellation;
        }

    }
}
