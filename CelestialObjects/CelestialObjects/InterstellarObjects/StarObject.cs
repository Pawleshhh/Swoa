namespace CelestialObjects
{
    public abstract record StarObject : CelestialObject
    {
        public string SpectralClass { get; init; } = string.Empty;
        public virtual double AbsoluteMagnitude { get; init; }
    }
}