namespace CelestialObjects
{
    public record OutsideStarObject : StarObject, IConstellationMember
    {
        public string Constellation { get; init; } = string.Empty;
    }
}