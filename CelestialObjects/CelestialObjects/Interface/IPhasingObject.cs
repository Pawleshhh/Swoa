namespace CelestialObjects
{
    public interface IPhasingObject
    {
        int PhaseCount { get; }

        string PhaseName { get; }

        double SurfaceIllumination { get; }
    }
}