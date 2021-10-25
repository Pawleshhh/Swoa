using Astronomy.Units;
using CelestialObjects;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace SwoaDatabaseAPI
{
    public class SwoaSqliteDb : SwoaDb
    {

        #region Constructors

        private SwoaSqliteDb() : base()
        {

        }

        #endregion

        #region Fields

        private static SwoaSqliteDb swoaSqliteDb;

        #endregion

        #region Properties

        public static SwoaSqliteDb SwoaSqliteDbSingleton => swoaSqliteDb ??= new SwoaSqliteDb();

        #endregion

        #region Methods

        public override IEnumerable<CelestialObject> GetCelestialObjects(string query)
        {
            using(var connection = new SqliteConnection($"Data Source={path}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = query;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        double x = reader.GetDouble(7);
                        double ra = (x / 24.0) * 360.0;
                        EquatorialCoordinates eqCoords = new EquatorialCoordinates(reader.GetDouble(8), ra);
                        var (alt, az) = Convert(ra, eqCoords.Declination, DateTime.UtcNow, 53.44101976999434, 14.55075611865506);
                        HorizonCoordinates horizonCoords = new HorizonCoordinates(alt, az);

                        if (alt > 0)
                        {
                            var name = GetName(reader);
                            double sundist = reader.GetDouble(9);
                            double mag = reader.GetDouble(11);
                            double absmag = reader.GetDouble(12);
                            //string spect = reader.GetString(13);
                            //string con = reader.GetString(14);
                            OutsideStarObject outsideStarObject = new OutsideStarObject()
                            {
                                Name = name,
                                EquatorialCoordinates = eqCoords,
                                HorizontalCoordinates = horizonCoords,
                                VisualMagnitude = mag
                            };

                            yield return outsideStarObject;
                        }

                    }
                }
            }
        }

        private string GetName(SqliteDataReader reader)
        {
            for (int i = 6; i >= 1; i--)
            {
                if (!reader.IsDBNull(i))
                    return reader.GetString(i);
            }

            return string.Empty;
        }

        private static readonly DateTime J2000 = new DateTime(2000, 1, 1);

        /// <summary>
        /// Converts RA and DEC coordinates to ALT and AZ coordinates.
        /// </summary>
        /// <param name="ra">RA in degrees.</param>
        /// <param name="dec">DEC in degrees.</param>
        /// <param name="date">Date and UTC time in given location.</param>
        /// <param name="latitude">Latitude of given location in degrees.</param>
        /// <param name="longitude">Longitude of given location in degrees (east positive).</param>
        private static (double alt, double az) Convert(double ra, double dec, DateTime date, double latitude, double longitude)
        {
            double daysFromJ2000 = (date - J2000).TotalDays;

            double lst = (100.46 + 0.985647 * daysFromJ2000 + longitude + 15 * date.TimeOfDay.TotalHours) + 360.0;

            double ha = lst - ra;
            if (ha < 0)
                ha += 360.0;

            double dec_rad = DegToRad(dec),
                lat_rad = DegToRad(latitude),
                ha_rad = DegToRad(ha);

            double sin_alt = Sin(dec_rad) * Sin(lat_rad) + Cos(dec_rad) * Cos(lat_rad) * Cos(ha_rad);
            double alt = Asin(sin_alt);

            double cos_az = (Sin(dec_rad) - Sin(alt) * Sin(lat_rad)) / (Cos(alt) * Cos(lat_rad));
            double az = RadToDeg(Acos(cos_az));

            if (Sin(ha_rad) >= 0.0)
                az = 360.0 - az;

            return (RadToDeg(alt), az);
        }

        private static double DegToRad(double a)
            => a * PI / 180.0;

        private static double RadToDeg(double a)
            => a * 180.0 / PI;

        #endregion

    }
}
