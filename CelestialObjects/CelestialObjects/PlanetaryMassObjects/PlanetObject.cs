namespace CelestialObjects
{
    public record PlanetObject : PlanetaryMassObject, IPhasingObject, IRingedObject
    {
        public PlanetType PlanetType { get; init; }

        public int SatelliteCount { get; init; }

        public int RingCount { get; init; }

        public int PhaseCount { get; init; }
        public string PhaseName { get; init; } = string.Empty;
        public double SurfaceIllumination { get; init; }
    }
}