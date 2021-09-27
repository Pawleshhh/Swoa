using System;

namespace CelestialObjects
{
    public interface IOrbitingObject : IRotatingObject
    {
        TimeSpan LengthOfYear { get; }

        double OrbitalVelocity { get; }
        double Eccentricity { get; }
        double SemiMajorAxis { get; }
        double Inclination { get; }
        double Obliquity { get; }

        CelestialObject OrbitedObject { get; }
        double DistanceToOrbitedObject { get; }
    }
}