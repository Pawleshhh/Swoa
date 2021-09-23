
namespace Astronomy.CelestialObjects.Properties
{
    public class DistanceProperty : PropertyBase<double>
    {

        public DistanceProperty(string name, double distance, DistanceUnit unit)
            : base(name, distance)
        {
            if (!Enum.IsDefined(typeof(DistanceUnit), unit))
                throw new ArgumentException(nameof(unit));

            DistanceUnit = unit;
        }

        public DistanceUnit DistanceUnit { get; }

    }
}
