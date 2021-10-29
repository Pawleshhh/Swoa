using Astronomy;
using CelestialObjects;
using SwoaDatabaseAPI;
using System;
using System.Collections.Generic;

namespace Swoa
{
    public class CelestialObjectManager
    {

        #region Constructors
        public CelestialObjectManager(ICelestialObjectCollection celestialObjects, SwoaDb swoaDb)
        {
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));
        }
        #endregion

        #region Fields

        private readonly ICelestialObjectCollection celestialObjects;
        private readonly SwoaDb swoaDb;

        private readonly CelestialObjectReviewer mainReviewer = new CelestialObjectReviewer();
        //private readonly CelestialObjectReviewer customReviewer = new CelestialObjectReviewer();

        #endregion

        #region Properties

        public IReadOnlyCollection<CelestialObject> CelestialObjects => celestialObjects;

        #endregion

        #region Events

        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Added
        {
            add => celestialObjects.Added += value;
            remove => celestialObjects.Added -= value;
        }
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Removed
        {
            add => celestialObjects.Removed += value;
            remove => celestialObjects.Removed -= value;
        }
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Cleared
        {
            add => celestialObjects.Cleared += value;
            remove => celestialObjects.Cleared -= value;
        }

        #endregion

        #region Methods

        public bool Add(CelestialObject celestialObject)
        {
            celestialObjects.Add(celestialObject);

            return true;
        }

        public bool Remove(CelestialObject celestialObject)
        {
            return celestialObjects.Remove(celestialObject);
        }

        public void Clear()
        {
            celestialObjects.Clear();
        }

        public bool CanBeAdded(CelestialObject celestialObject)
        {
            return celestialObject != null;
        }

        public void Update()
        {
            var records = SwoaSqliteDb.SwoaSqliteDbSingleton.GetSwoaDbRecords("mag <= 7", SwoaDbRecordType.Star);

            foreach (var record in records)
            {
                var ra = record.Ra / 24.0 * 360.0;

                var (alt, az) = CoordinatesConverter.EquatorialToHorizonCoords(ra, record.Dec, DateTime.UtcNow, 53.4410141708595, 14.550730731716628);

                if (alt <= 0)
                    continue;

                var celestialObj = new OutsideStarObject()
                {
                    HorizontalCoordinates = new Astronomy.Units.HorizonCoordinates(alt, az),
                    VisualMagnitude = record.Mag
                };

                Add(celestialObj);
            }
        }

        #endregion

    }
}
