using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class Units
    {

        #region Constructors
        private Units(string unitName, string unitShort)
        {
            UnitName = unitName ?? throw new ArgumentNullException(nameof(unitName));
            UnitShort = unitShort ?? throw new ArgumentNullException(nameof(unitShort));
        }
        #endregion

        #region Properties

        public string UnitName { get; }
        public string UnitShort { get; }

        #endregion

        #region Units

        public static Units LightYears { get; } = new Units("Light Years", "l.y.");
        public static Units Kilometers { get; } = new Units("Kilometers", "km");

        #endregion

    }
}
