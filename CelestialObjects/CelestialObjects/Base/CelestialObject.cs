using Astronomy.Units;
using System;

namespace CelestialObjects
{
    public abstract record CelestialObject
    {
        public string Name { get; init; } = string.Empty;
        public EquatorialCoordinates EquatorialCoordinates { get; init; }
        public HorizonCoordinates HorizontalCoordinates { get; init; }
        public double VisualMagnitude { get; init; }
        public double DistanceToSun { get; init; }
        public double DistanceToEarth { get; init; }
        public TimeSpan RisesAt { get; init; }
        public TimeSpan SetsAt { get; init; }
    }
}