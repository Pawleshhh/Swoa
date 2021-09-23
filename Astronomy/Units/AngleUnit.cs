using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.Units
{
    public sealed class AngleUnit : Unit
    {

        private AngleUnit(string unitName, string unitSignature)
            : base(unitName, unitSignature) { }

        public static AngleUnit Degree { get; } = new AngleUnit("Degree", string.Empty);
        public static AngleUnit Radian { get; } = new AngleUnit("Radian", string.Empty);

    }
}
