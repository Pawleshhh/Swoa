using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class PropertyChangedHelper
    {

        public static bool SetProperty<T>(ref T property, T value, Action<T, T> eventAction)
        {
            if (property.Equals(value))
                return false;

            var prev = property;
            property = value;

            eventAction(prev, value);

            return true;
        }

    }
}
