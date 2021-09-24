using Astronomy.CelestialObjects.Properties;
using System;
using System.Collections.Generic;

namespace Astronomy.CelestialObjects
{
    public class MoonObjectBuilder : CelestialObjectBuilder
    {

        public TimeSpan RisesAt { get; set; }
        public TimeSpan SetsAt { get; set; }
        public double DistanceToSun { get; set; }
        public double DistanceToEarth { get; set; }
        public double DistanceToOrbitedPlanet { get; set; }
        public double AverageDistanceToSun { get; set; }
        public double AverageDistanceToEarth { get; set; }

        public double Peryhelium { get; set; }
        public double Aphelium { get; set; }
        public double Perygeum { get; set; }
        public double Apogeum { get; set; }

        public TimeSpan LengthOfDay { get; set; }
        public double LengthOfDayEarthRelative { get; set; }
        public TimeSpan LengthOfYear { get; set; }
        public double LengthOfYearEarthRelative { get; set; }

        public double ApparentDiameter { get; set; }

        public double Radius { get; set; }
        public double RadiusEarthRelative { get; set; }

        public double Volume { get; set; }
        public double VolumeEarthRelative { get; set; }

        public double Mass { get; set; }
        public double MassEarthRelative { get; set; }

        public double Density { get; set; }
        public double DensityEarthRelative { get; set; }

        public double SurfaceArea { get; set; }
        public double SurfaceAreaEarthRelative { get; set; }

        public double Gravity { get; set; }

        public double OrbitalVelocity { get; set; }

        public double MaximumSurfaceTemperature { get; set; }
        public double MinimumSurfaceTemperature { get; set; }
        public double AverageSurfaceTemperature { get; set; }

        public double Obliquity { get; set; }
        public double Eccentricity { get; set; }
        public double Inclination { get; set; }

        public bool Atmosphere { get; set; }

        public string Phase { get; set; } = string.Empty;
        public double PhaseIllumination { get; set; }

        protected override Dictionary<string, IProperty> GetProperties()
        {
            var properties = new Dictionary<string, IProperty>(new KeyValuePair<string, IProperty>[]
            {
                AllProperties.RisesAt.ToKeyValuePair(RisesAt),
                AllProperties.SetsAt.ToKeyValuePair(SetsAt),
                AllProperties.DistanceToSun.ToKeyValuePair(DistanceToSun),
                AllProperties.DistanceToEarth.ToKeyValuePair(DistanceToEarth),
                AllProperties.DistanceToOrbitedPlanet.ToKeyValuePair(DistanceToOrbitedPlanet),
                AllProperties.AvgDistanceToSun.ToKeyValuePair(AverageDistanceToSun),
                AllProperties.AvgDistanceToEarth.ToKeyValuePair(AverageDistanceToEarth),
                AllProperties.Peryhelium.ToKeyValuePair(Peryhelium),
                AllProperties.Aphelium.ToKeyValuePair(Aphelium),
                AllProperties.Perygeum.ToKeyValuePair(Perygeum),
                AllProperties.Apogeum.ToKeyValuePair(Apogeum),
                AllProperties.LengthOfDay.ToKeyValuePair(LengthOfDay),
                AllProperties.LengthOfYear.ToKeyValuePair(LengthOfYear),
                AllProperties.LengthOfDayEarthRelative.ToKeyValuePair(LengthOfDayEarthRelative),
                AllProperties.LengthOfYearEarthRelative.ToKeyValuePair(LengthOfYearEarthRelative),
                AllProperties.ApparentDiameter.ToKeyValuePair(ApparentDiameter),
                AllProperties.Radius.ToKeyValuePair(Radius),
                AllProperties.RadiusEarthRelative.ToKeyValuePair(RadiusEarthRelative),
                AllProperties.Volume.ToKeyValuePair(Volume),
                AllProperties.VolumeEarthRelative.ToKeyValuePair(VolumeEarthRelative),
                AllProperties.Mass.ToKeyValuePair(Mass),
                AllProperties.MassEarthRelative.ToKeyValuePair(MassEarthRelative),
                AllProperties.Density.ToKeyValuePair(Density),
                AllProperties.DensityEarthRelative.ToKeyValuePair(DensityEarthRelative),
                AllProperties.SurfaceArea.ToKeyValuePair(SurfaceArea),
                AllProperties.SurfaceAreaEarthRelative.ToKeyValuePair(SurfaceAreaEarthRelative),
                AllProperties.Gravity.ToKeyValuePair(Gravity),
                AllProperties.OrbitalVelocity.ToKeyValuePair(OrbitalVelocity),
                AllProperties.MaximumSurfaceTemperature.ToKeyValuePair(MaximumSurfaceTemperature),
                AllProperties.MinimumSurfaceTemperature.ToKeyValuePair(MinimumSurfaceTemperature),
                AllProperties.AverageSurfaceTemperature.ToKeyValuePair(AverageSurfaceTemperature),
                AllProperties.Obliquity.ToKeyValuePair(Obliquity),
                AllProperties.Eccentricity.ToKeyValuePair(Eccentricity),
                AllProperties.Inclination.ToKeyValuePair(Inclination),
                AllProperties.Atmosphere.ToKeyValuePair(Atmosphere),
                AllProperties.Phase.ToKeyValuePair(Phase),
                AllProperties.PhaseIllumination.ToKeyValuePair(PhaseIllumination)
            });

            return properties;
        }

    }
}
