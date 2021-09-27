namespace CelestialObjects
{
    public interface IMeasureableObject
    {
        double ApparentDiameter { get; }
        double Radius { get; }
        double Volume { get; }
        double Mass { get; }
        double Density { get; }
        double SurfaceArea { get; }
        double Gravity { get; }
        double Temperature { get; }
    }
}