namespace CelestialObjects
{
    public record NaturalSatelliteObject : PlanetaryMassObject, IPhasingObject
    {
        public int PhaseCount { get; init; }
        public string PhaseName { get; init; } = string.Empty;
        public double SurfaceIllumination { get; init; }

        public override CelestialObject OrbitedObject { get => OrbitedPlanet; init => OrbitedPlanet = (PlanetObject)value; }

        public PlanetObject OrbitedPlanet { get; init; }
    }
}