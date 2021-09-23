using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.Units
{
    public sealed class LengthUnit : Unit
    {

        private LengthUnit(string unitName, string unitSignature)
            : base(unitName, unitSignature) { }

        public static LengthUnit Kilometer { get; } = new LengthUnit("Kilometer", "km");
        public static LengthUnit Mile { get; } = new LengthUnit("Mile", "MI");

    }
}
