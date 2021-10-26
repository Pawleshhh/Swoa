using Astronomy;
using CelestialObjects;
using Swoa;
using SwoaDatabaseAPI;
using System;

namespace Swoa
{
    public class SwoaManager
    {

        #region Constructors

        public SwoaManager()
        {
            ICelestialObjectCollection celestialObjects = new CelestialObjectList();

            CelestialObjectManager = new CelestialObjectManager(celestialObjects);

            Initialize();
        }

        #endregion

        #region Properties

        public CelestialObjectManager CelestialObjectManager { get; }

        #endregion

        #region Methods

        private void Initialize()
        {
            var records = SwoaSqliteDb.SwoaSqliteDbSingleton.GetSwoaDbRecordsByMagnitude(7, DbCompareOperator.Less, SwoaDbRecordType.Star);

            foreach (var record in records)
            {
                var ra = record.Ra / 24.0 * 360.0;

                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, record.Dec, DateTime.UtcNow, 53.4410141708595, 14.550730731716628);

                var celestialObj = new OutsideStarObject()
                {
                    HorizontalCoordinates = new Astronomy.Units.HorizonCoordinates(alt, az),
                    VisualMagnitude = record.Mag
                };

                CelestialObjectManager.Add(celestialObj);
            }
        }

        #endregion

    }
}
