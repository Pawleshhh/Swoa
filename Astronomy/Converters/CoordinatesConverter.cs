using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Astronomy.MathHelper;
using static System.Math;

namespace Astronomy
{
    public static class CoordinatesConverter
    {


        private static readonly DateTime J2000 = new DateTime(2000, 1, 1);

        /// <summary>
        /// Converts RA and DEC coordinates to ALT and AZ coordinates.
        /// </summary>
        /// <param name="ra">RA in degrees.</param>
        /// <param name="dec">DEC in degrees.</param>
        /// <param name="date">Date and UTC time in given location.</param>
        /// <param name="latitude">Latitude of given location in degrees.</param>
        /// <param name="longitude">Longitude of given location in degrees (east positive).</param>
        public static (double alt, double az) EquatorialToHorizonCoords(double ra, double dec, DateTime date, double latitude, double longitude)
        {
            double daysFromJ2000 = (date - J2000).TotalDays;

            double lst = (100.46 + 0.985647 * daysFromJ2000 + longitude + 15 * date.TimeOfDay.TotalHours) + 360.0;

            double ha = lst - ra;
            if (ha < 0)
                ha += 360.0;

            double dec_rad = DegreesToRadians(dec),
                lat_rad = DegreesToRadians(latitude),
                ha_rad = DegreesToRadians(ha);

            double sin_alt = Sin(dec_rad) * Sin(lat_rad) + Cos(dec_rad) * Cos(lat_rad) * Cos(ha_rad);
            double alt = Asin(sin_alt);

            double cos_az = (Sin(dec_rad) - Sin(alt) * Sin(lat_rad)) / (Cos(alt) * Cos(lat_rad));
            double az = RadiansToDegrees(Acos(cos_az));

            if (Sin(ha_rad) >= 0.0)
                az = 360.0 - az;

            return (RadiansToDegrees(alt), az);
        }

        public static (double, double) HorizonToCartesianCoords(double r, HorizonCoordinates horizonCoords)
        {
            var az = horizonCoords.Azimuth;

            var x = r * CosD(az);
            var y = r * CosD(az);

            return (x, y);
        }

    }
}
