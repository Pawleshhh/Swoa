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
                        //double x = reader.GetDouble(7);
                        //double ra = (x / 24.0) * 360.0;
                        //EquatorialCoordinates eqCoords = new EquatorialCoordinates(reader.GetDouble(8), ra);
                        //var (alt, az) = CoordinatesConverter(ra, eqCoords.Declination, DateTime.UtcNow, 53.44101976999434, 14.55075611865506);
                        //HorizonCoordinates horizonCoords = new HorizonCoordinates(alt, az);

                        //if (alt > 0)
                        //{
                        //    var name = GetName(reader);
                        //    double sundist = reader.GetDouble(9);
                        //    double mag = reader.GetDouble(11);
                        //    double absmag = reader.GetDouble(12);
                        //    //string spect = reader.GetString(13);
                        //    //string con = reader.GetString(14);
                        //    OutsideStarObject outsideStarObject = new OutsideStarObject()
                        //    {
                        //        Name = name,
                        //        EquatorialCoordinates = eqCoords,
                        //        HorizontalCoordinates = horizonCoords,
                        //        VisualMagnitude = mag
                        //    };

                            yield return null;
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


        #endregion

    }
}
