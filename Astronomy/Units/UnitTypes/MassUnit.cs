using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.Units
{
    public sealed class MassUnit : Unit
    {

        private MassUnit(string unitName, string unitSignature)
            : base(unitName, unitSignature) { }

        public static MassUnit Kilogram { get; } = new MassUnit("Kilogram", "kg");
        public static MassUnit Ton { get; } = new MassUnit("Ton", "t");

    }
}
