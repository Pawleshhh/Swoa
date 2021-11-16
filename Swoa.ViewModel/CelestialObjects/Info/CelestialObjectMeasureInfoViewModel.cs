using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class CelestialObjectMeasureInfoViewModel<T> : CelestialObjectInfoViewModel<T>
    {

        #region Constructors
        public CelestialObjectMeasureInfoViewModel(string name, T value, Units unit) : base(name, value)
        {
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }
        #endregion

        #region Properties

        public Units Unit { get; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{base.ToString()} {Unit.UnitShort}";
        }

        #endregion

    }
}
