using System;

namespace CelestialObjects
{
    public abstract record PlanetaryMassObject : CelestialObject, IMeasureableObject, IOrbitingObject
    {
        public double ApparentDiameter { get; init; }
        public double Radius { get; init; }
        public double Volume { get; init; }
        public double Mass { get; init; }
        public double Density { get; init; }
        public double SurfaceArea { get; init; }
        public double Gravity { get; init; }
        public double Temperature { get; init; }

        public TimeSpan LengthOfYear { get; init; }
        public TimeSpan LengthOfDay { get; init; }

        public double OrbitalVelocity { get; init; }
        public double Eccentricity { get; init; }
        public double SemiMajorAxis { get; init; }
        public double Inclination { get; init; }
        public double Obliquity { get; init; }

        public virtual CelestialObject OrbitedObject { get; init; }
        public double DistanceToOrbitedObject { get; init; }
    }
}