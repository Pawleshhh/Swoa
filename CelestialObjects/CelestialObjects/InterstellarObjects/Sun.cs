using System;

namespace CelestialObjects
{
    public record Sun : StarObject, IRotatingObject
    {
        public TimeSpan LengthOfDay { get; init; }
    }
}