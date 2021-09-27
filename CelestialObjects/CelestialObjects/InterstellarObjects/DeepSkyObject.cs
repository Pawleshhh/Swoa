namespace CelestialObjects
{
    public record DeepSkyObject : CelestialObject, IConstellationMember
    {
        public string Constellation { get; init; } = string.Empty;

        public DeepSkyObjectType DeepSkyObjectType { get; init; }
    }
}