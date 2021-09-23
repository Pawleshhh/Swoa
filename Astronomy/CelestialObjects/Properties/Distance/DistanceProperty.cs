
namespace Astronomy.CelestialObjects.Properties
{
    public class DistanceProperty : PropertyBase<double>
    {

        public DistanceProperty(double distance, DistanceUnit unit)
            : base("Distance", distance)
        {
            if (!Enum.IsDefined(typeof(DistanceUnit), unit))
                throw new ArgumentException(nameof(unit));

            DistanceUnit = unit;
        }

        public DistanceUnit DistanceUnit { get; }

    }
}
