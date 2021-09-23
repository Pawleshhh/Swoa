using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.Units
{
    public sealed class DistanceUnit : Unit
    {

        private DistanceUnit(string unitName, string unitSignature)
            : base(unitName, unitSignature) { }

        public static DistanceUnit AstronomicalUnit { get; } = new DistanceUnit("Astronomical Unit", "AU");
        public static DistanceUnit LightYear { get; } = new DistanceUnit("Light Year", "l.y.");

    }
}
