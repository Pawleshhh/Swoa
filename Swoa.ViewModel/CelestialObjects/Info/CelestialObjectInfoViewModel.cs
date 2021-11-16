using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class CelestialObjectInfoViewModel<T> : NotifyPropertyChanges, ICelestialObjectInfoViewModel<T>
    {

        #region Constructors
        public CelestialObjectInfoViewModel(string name, T value)
        {
            if (!string.IsNullOrEmpty(name))
                Name = name;
            else
                throw new ArgumentException(nameof(name));

            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        #region Properties

        public string Name { get; }
        public T Value { get; }

        object ICelestialObjectInfoViewModel.Value => Value;

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            if (obj is ICelestialObjectInfoViewModel<T> info)
                return Value.Equals(info.Value);

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion

    }
}
