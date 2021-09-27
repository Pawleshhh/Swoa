namespace CelestialObjects
{
    public abstract record StarObject : CelestialObject
    {
        public string SpectralClass { get; init; } = string.Empty;
        public double AbsoluteMagnitude { get; init; }
    }
}