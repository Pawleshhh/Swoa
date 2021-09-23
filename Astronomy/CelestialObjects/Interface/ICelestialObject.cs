using Astronomy.CelestialObjects.Properties;
using Astronomy.Coordinates;

namespace Astronomy.CelestialObjects
{
    public interface ICelestialObject : IEquatable<ICelestialObject>
    {

        string Name { get; }

        double VisualMagnitude { get; }

        HorizonCoordinates HorizonCoordinates { get; }
        EquatorialCoordinates EquatorialCoordinates { get; }

        IReadOnlyDictionary<string, IProperty> Properties { get; }

    }
}
