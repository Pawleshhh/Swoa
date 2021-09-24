using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class StarObjectType : CelestialObjectType
    {
        protected StarObjectType(string name) : base(name)
        {
        }

        public static StarObjectType DefaultStar { get; } = new StarObjectType("Default star");
        public static StarObjectType Sun { get; } = new StarObjectType("The Sun");

        public override CelestialObjectType GetDefault()
        {
            return DefaultStar;
        }

    }
}
