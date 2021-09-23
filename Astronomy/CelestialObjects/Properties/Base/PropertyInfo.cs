using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects.Properties
{
    public class PropertyInfo<T>
        where T : IComparable<T>
    {

        public PropertyInfo(Func<T, IProperty> buildProperty, string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException(nameof(key));

            this.buildProperty = buildProperty ?? throw new ArgumentNullException(nameof(buildProperty));
            Key = key;
        }

        private Func<T, IProperty> buildProperty;

        public string Key { get; }

        public IProperty BuildProperty(T value)
            => buildProperty.Invoke(value);

    }
}
