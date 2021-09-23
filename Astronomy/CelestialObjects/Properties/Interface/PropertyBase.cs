
namespace Astronomy.CelestialObjects.Properties
{
    public class PropertyBase<T> : IProperty<T>
    {

        public PropertyBase(string name, T value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Name = name;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Name { get; }

        object IProperty.Value => Value;

        public T Value { get; }

        public bool Equals(IProperty? other)
        {
            if (other == null) 
                return false;

            return Equals((IProperty<T>)other);
        }

        public bool Equals(IProperty<T>? other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Name.Equals(other.Name) && Value.Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            if (obj is IProperty<T> property)
                return Equals(property);

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * Value.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
