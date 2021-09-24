using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class CelestialObjectType
    {

        protected CelestialObjectType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public static CelestialObjectType None { get; } = new CelestialObjectType(string.Empty);

    }
}
