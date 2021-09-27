using System;

namespace CelestialObjects
{
    public interface IRotatingObject
    {
        TimeSpan LengthOfDay { get; }
    }
}