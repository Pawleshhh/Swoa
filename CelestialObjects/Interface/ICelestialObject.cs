﻿using Astronomy.Coordinates;

namespace CelestialObjects
{
    public interface ICelestialObject : IEquatable<ICelestialObject>
    {

        string Name { get; }

        double VisualMagnitude { get; }

        HorizonCoordinates HorizonCoordinates { get; }
        EquatorialCoordinates EquatorialCoordinates { get; }

    }
}
