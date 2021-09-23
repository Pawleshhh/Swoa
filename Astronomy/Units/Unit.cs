using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.Units
{
    public class Unit : IEquatable<Unit>
    {

        #region Constructors

        protected Unit(string unitName, string unitSignature)
        {
            UnitName = unitName ?? throw new ArgumentNullException(nameof(unitName));
            UnitSignature = unitSignature ?? throw new ArgumentNullException(nameof(unitSignature));
        }

        #endregion

        #region Static properties

        public static Unit NoneProperty { get; } = new Unit("", "");

        #endregion

        #region Properties

        public string UnitName { get; }
        public string UnitSignature { get; }

        #endregion

        #region Methods

        public bool Equals(Unit? unit)
        {
            if (unit == null)
                return false;

            if (ReferenceEquals(this, unit))
                return true;

            return UnitName.Equals(unit.UnitName) && UnitSignature.Equals(unit.UnitSignature);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Unit unit)
                return Equals(unit);

            return false;
        }

        public override int GetHashCode()
        {
            return UnitName.GetHashCode() * UnitSignature.GetHashCode() * 13;
        }

        public override string ToString()
        {
            return UnitName;
        }

        #endregion

    }
}
