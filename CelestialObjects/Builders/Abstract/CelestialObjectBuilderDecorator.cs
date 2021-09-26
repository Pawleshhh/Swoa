using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public abstract class CelestialObjectBuilderDecorator : ICelestialObjectBuilder
    {

        #region Constructors

        public CelestialObjectBuilderDecorator(ICelestialObjectBuilder celestialObjectBuilder)
        {
            decorator = celestialObjectBuilder ?? throw new ArgumentNullException(nameof(celestialObjectBuilder));
        }

        #endregion

        #region Fields

        private readonly ICelestialObjectBuilder decorator;

        #endregion

        public CelestialObject Build()
        {
            return new CelestialObject(GetProperties());
        }

        public Dictionary<string, IProperty> GetProperties()
        {
            var properties = new Dictionary<string, IProperty>(decorator.GetProperties());

            foreach (var keyValuePair in GetDecoratorProperties())
                properties.AddKeyValuePair(keyValuePair);

            return properties;
        }

        protected abstract Dictionary<string, IProperty> GetDecoratorProperties();
    }

}
