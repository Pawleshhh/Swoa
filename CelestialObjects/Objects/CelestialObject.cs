using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialObjects
{
    public class CelestialObject : IEquatable<CelestialObject>
    {
        #region Constructors

        public CelestialObject(Dictionary<string, IProperty> properties)
        {
            this.properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        #endregion

        #region Fields

        private readonly Dictionary<string, IProperty> properties;

        #endregion

        #region Properties

        public IProperty this[string key] => properties[key];

        #endregion

        #region Methods

        public bool ContainsProperty(string key)
        {
            return properties.ContainsKey(key);
        }

        public virtual bool Equals(CelestialObject? other)
        {
            if (other == null)
                return false;

            foreach (var key in properties.Keys)
            {
                if (!other.ContainsProperty(key) || !properties[key].Equals(other[key]))
                    return false;
            }

            return true;
        }

        #endregion

    }
}
