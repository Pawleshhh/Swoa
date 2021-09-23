
using Astronomy.Units;
using System;

namespace Astronomy.CelestialObjects.Properties
{
    public class Property<T, U> : IProperty, IEquatable<Property<T, U>>
        where T : IComparable<T>
        where U : Unit
    {

        public Property(string name, T value, U unit)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Name = name;
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public string Name { get; }

        object IProperty.Value => Value;

        Unit IProperty.Unit => Unit;

        public T Value { get; }

        public U Unit { get; }

        public bool Equals(IProperty? other)
        {
            if (other == null)
                return false;

            return Equals((Property<T, U>)other);
        }

        public bool Equals(Property<T, U>? other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Name.Equals(other.Name) && Value.Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Property<T, U> property)
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
