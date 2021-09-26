using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities;

namespace CelestialObjects
{
    public class CelestialObjectBuilder : ICelestialObjectBuilder
    {

        #region Properties
        public string Name { get; set; } = string.Empty;
        public EquatorialCoordinates EquatorialCoordinates { get; set; }
        public HorizontalCoordinates HorizontalCoordinates { get; set; }
        public double VisualMagnitude { get; set; }
        #endregion

        #region Methods

        public CelestialObject Build()
        {
            return new CelestialObject(GetProperties());
        }

        public virtual Dictionary<string, IProperty> GetProperties()
        {
            var properties = new Dictionary<string, IProperty>();
            properties.AddKeyValuePair(GetKeyValuePair(nameof(Name), Name));
            properties.AddKeyValuePair(GetKeyValuePairNormalized(nameof(EquatorialCoordinates), EquatorialCoordinates));
            properties.AddKeyValuePair(GetKeyValuePairNormalized(nameof(HorizontalCoordinates), HorizontalCoordinates));
            properties.AddKeyValuePair(GetKeyValuePairNormalized(nameof(VisualMagnitude), VisualMagnitude));

            return properties;
        }

        public static KeyValuePair<string, IProperty> GetKeyValuePairNormalized<T>(string key, T value)
            where T : IComparable<T>
            => GetKeyValuePair(key, GetNormalizedPropertyName(key), value);

        public static KeyValuePair<string, IProperty> GetKeyValuePair<T>(string key, T value)
            where T : IComparable<T>
            => GetKeyValuePair(key, key, value);

        public static KeyValuePair<string, IProperty> GetKeyValuePair<T>(string key, string name, T value)
            where T : IComparable<T>
            => new KeyValuePair<string, IProperty>(key, new Property<T>(name, value));

        public static string GetNormalizedPropertyName(string name)
        {
            string[] split = Regex.Split(name, @"(?<!^)(?=[A-Z])");

            return string.Join(' ', split);
        }

        #endregion
    }
}
