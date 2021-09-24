using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class PlanetObjectBuilder : CelestialObjectBuilder
    {

        public TimeSpan RisesAt { get; set; }
        public TimeSpan SetsAt { get; set; }
        public double DistanceToSun { get; set; }
        public double DistanceToEarth { get; set; }
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

        public override CelestialObjectBase BuildCelestialObject()
        {
            throw new NotImplementedException();
        }
    }
}
