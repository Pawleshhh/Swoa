using Astronomy.CelestialObjects.Properties;
using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class CelestialObjectBase : ICelestialObject
    {

        #region Constructors

        public CelestialObjectBase(string name, double visualMagnitude, HorizonCoordinates horizonCoordinates, EquatorialCoordinates equatorialCoordinates,
            Dictionary<string, IProperty> properties)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Name = name;
            VisualMagnitude = visualMagnitude;
            HorizonCoordinates = horizonCoordinates;
            EquatorialCoordinates = equatorialCoordinates;

            this.properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        #endregion

        #region Fields

        private readonly Dictionary<string, IProperty> properties;

        #endregion

        public IProperty this[string key] => properties[key];

        public string Name { get; }

        public double VisualMagnitude { get; }

        public HorizonCoordinates HorizonCoordinates { get; }

        public EquatorialCoordinates EquatorialCoordinates { get; }

        public bool ContainsProperty(string key)
        {
            return properties.ContainsKey(key);
        }

        public virtual bool Equals(ICelestialObject? other)
        {
            if (other == null)
                return false;

            if (Name == other.Name && VisualMagnitude == other.VisualMagnitude &&
                HorizonCoordinates.Equals(other.HorizonCoordinates) &&
                EquatorialCoordinates.Equals(other.EquatorialCoordinates))
            {
                foreach (var key in properties.Keys)
                {
                    if (!other.ContainsProperty(key) || !properties[key].Equals(other[key]))
                        return false;
                }

                return true;
            }

            return false;
        }
    }
}
