using Astronomy.CelestialObjects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Astronomy.CelestialObjects
{
    public class SunBuilder : StarObjectBuilder
    {

        public TimeSpan RisesAt { get; set; }
        public TimeSpan SetsAt { get; set; }

        public double ApparentDiameter { get; set; }
        public double DistanceToEarth { get; set; }
        public double Radius { get; set; }
        public double RadiusEarthRelative { get; set; }
        public double Volume { get; set; }
        public double VolumeEarthRelative { get; set; }
        public double Mass { get; set; }
        public double MassEarthRelative { get; set; }
        public double Density { get; set; }
        public double SurfaceArea { get; set; }
        public double SurfaceAreaEarthRelative { get; set; }
        public double Gravity { get; set; }

        public TimeSpan LengthOfDay { get; set; }
        public double LengthOfDayEarthRelative { get; set; }

        public double MaximumSurfaceTemperature { get; set; }
        public double MinimumSurfaceTemperature { get; set; }
        public double AverageSurfaceTemperature { get; set; }

        public int Age { get; set; }

        protected override Dictionary<string, IProperty> GetProperties()
        {
            var properties = base.GetProperties();

            properties.Remove(AllProperties.AbsoluteMagnitude.Key);
            properties.Remove(AllProperties.DistanceToSun.Key);

            properties.AddKeyValuePair(AllProperties.RisesAt.ToKeyValuePair(RisesAt));
            properties.AddKeyValuePair(AllProperties.SetsAt.ToKeyValuePair(SetsAt));
            properties.AddKeyValuePair(AllProperties.ApparentDiameter.ToKeyValuePair(ApparentDiameter));
            properties.AddKeyValuePair(AllProperties.DistanceToEarth.ToKeyValuePair(DistanceToEarth));
            properties.AddKeyValuePair(AllProperties.Radius.ToKeyValuePair(Radius));
            properties.AddKeyValuePair(AllProperties.RadiusEarthRelative.ToKeyValuePair(RadiusEarthRelative));
            properties.AddKeyValuePair(AllProperties.Volume.ToKeyValuePair(Volume));
            properties.AddKeyValuePair(AllProperties.VolumeEarthRelative.ToKeyValuePair(VolumeEarthRelative));
            properties.AddKeyValuePair(AllProperties.Mass.ToKeyValuePair(Mass));
            properties.AddKeyValuePair(AllProperties.MassEarthRelative.ToKeyValuePair(MassEarthRelative));
            properties.AddKeyValuePair(AllProperties.Density.ToKeyValuePair(Density));
            properties.AddKeyValuePair(AllProperties.SurfaceArea.ToKeyValuePair(SurfaceArea));
            properties.AddKeyValuePair(AllProperties.SurfaceAreaEarthRelative.ToKeyValuePair(SurfaceAreaEarthRelative));
            properties.AddKeyValuePair(AllProperties.Gravity.ToKeyValuePair(Gravity));
            properties.AddKeyValuePair(AllProperties.LengthOfDay.ToKeyValuePair(LengthOfDay));
            properties.AddKeyValuePair(AllProperties.LengthOfDayEarthRelative.ToKeyValuePair(LengthOfDayEarthRelative));
            properties.AddKeyValuePair(AllProperties.MaximumSurfaceTemperature.ToKeyValuePair(MaximumSurfaceTemperature));
            properties.AddKeyValuePair(AllProperties.MinimumSurfaceTemperature.ToKeyValuePair(MinimumSurfaceTemperature));
            properties.AddKeyValuePair(AllProperties.AverageSurfaceTemperature.ToKeyValuePair(AverageSurfaceTemperature));
            properties.AddKeyValuePair(AllProperties.Age.ToKeyValuePair(Age));

            return properties;
        }

    }
}
