using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects.Properties
{
    public static class Properties
    {

        public static PropertyInfo<int> AbsoluteMagnitude { get; } = GetPropertyInfo<int>("Absolute magnitude");

        public static PropertyInfo<TimeSpan> RisesAt { get; } = GetPropertyInfo<TimeSpan>("Rises at");
        public static PropertyInfo<TimeSpan> SetsAt { get; } = GetPropertyInfo<TimeSpan>("Sets at");

        public static PropertyInfo<double> DistanceToSun { get; } = GetPropertyInfo<double>("Distance to Sun");
        public static PropertyInfo<double> DistanceToEarth { get; } = GetPropertyInfo<double>("Distance to Earth");
        public static PropertyInfo<double> AvgDistanceToSun { get; } = GetPropertyInfo<double>("Average distance to Sun");
        public static PropertyInfo<double> AvgDistanceToEarth { get; } = GetPropertyInfo<double>("Average distance to Earth");

        public static PropertyInfo<double> Peryhelium { get; } = GetPropertyInfo<double>("Peryhelium");
        public static PropertyInfo<double> Aphelium { get; } = GetPropertyInfo<double>("Aphelium");
        public static PropertyInfo<double> Perygeum { get; } = GetPropertyInfo<double>("Perygeum");
        public static PropertyInfo<double> Apogeum { get; } = GetPropertyInfo<double>("Apogeum");

        public static PropertyInfo<TimeSpan> LengthOfDay { get; } = GetPropertyInfo<TimeSpan>("Length of day");
        public static PropertyInfo<double> LengthOfDayEarthRelative { get; } = GetPropertyInfo<double>("Length of day relative to Earth");
        public static PropertyInfo<TimeSpan> LengthOfYear { get; } = GetPropertyInfo<TimeSpan>("Length of year");
        public static PropertyInfo<double> LengthOfYearEarthRelative { get; } = GetPropertyInfo<double>("Length of year relative to Earth");

        public static PropertyInfo<double> ApparentDiameter { get; } = GetPropertyInfo<double>("Apparent diameter");

        public static PropertyInfo<double> Radius { get; } = GetPropertyInfo<double>("Radius");
        public static PropertyInfo<double> RadiusEarthRelative { get; } = GetPropertyInfo<double>("Radius relative to Earth");

        public static PropertyInfo<double> Volume { get; } = GetPropertyInfo<double>("Volume");
        public static PropertyInfo<double> VolumeEarthRelative { get; } = GetPropertyInfo<double>("Volume relative to Earth");

        public static PropertyInfo<double> Mass { get; } = GetPropertyInfo<double>("Mass");
        public static PropertyInfo<double> MassEarthRelative { get; } = GetPropertyInfo<double>("Mass relative to Earth");

        public static PropertyInfo<double> Density { get; } = GetPropertyInfo<double>("Density");
        public static PropertyInfo<double> DensityEarthRelative { get; } = GetPropertyInfo<double>("Density relative to Earth");

        public static PropertyInfo<double> SurfaceArea { get; } = GetPropertyInfo<double>("Surface area");
        public static PropertyInfo<double> SurfaceAreaEarthRelative { get; } = GetPropertyInfo<double>("Surface area relative to Earth");

        public static PropertyInfo<double> Gravity { get; } = GetPropertyInfo<double>("Gravity");

        public static PropertyInfo<double> OrbitalVelocity { get; } = GetPropertyInfo<double>("Orbital velocity");

        public static PropertyInfo<double> MaximumSurfaceTemperature { get; } = GetPropertyInfo<double>("Maximum surface temperature");
        public static PropertyInfo<double> MinimumSurfaceTemperature { get; } = GetPropertyInfo<double>("Minimum surface temperature");
        public static PropertyInfo<double> AverageSurfaceTemperature { get; } = GetPropertyInfo<double>("Average surface temperature");

        public static PropertyInfo<double> Obliquity { get; } = GetPropertyInfo<double>("Obliquity");
        public static PropertyInfo<double> Eccentricity { get; } = GetPropertyInfo<double>("Eccentricity");
        public static PropertyInfo<double> Inclination { get; } = GetPropertyInfo<double>("Inclination");

        public static PropertyInfo<bool> Atmosphere { get; } = GetPropertyInfo<bool>("Atmosphere");

        public static PropertyInfo<string> Phase { get; } = GetPropertyInfo<string>("Phase");
        public static PropertyInfo<double> PhaseIllumination { get; } = GetPropertyInfo<double>("Phase illumination");

        //TODO: Constellation!!!

        public static PropertyInfo<string> SpectralClass { get; } = GetPropertyInfo<string>("Spectral class");
        public static PropertyInfo<string> DeepSkyObjectType { get; } = GetPropertyInfo<string>("Deep sky object type");

        //TODO: Orbited planet!!!

        public static PropertyInfo<double> DistanceToOrbitedPlanet { get; } = GetPropertyInfo<double>("Distance to orbited planet");

        public static PropertyInfo<int> StarCount { get; } = GetPropertyInfo<int>("Star count");
        public static PropertyInfo<int> RingCount { get; } = GetPropertyInfo<int>("Ring count");
        public static PropertyInfo<int> SatelliteCount { get; } = GetPropertyInfo<int>("Satellite count");

        public static PropertyInfo<string> Satellites { get; } = GetPropertyInfo<string>("Satellites");

        private static PropertyInfo<T> GetPropertyInfo<T>(string name, [CallerMemberName]string key = "noKey") where T : IComparable<T>
            => new PropertyInfo<T>(n => new Property<T>(name, n), key);
    }
}
