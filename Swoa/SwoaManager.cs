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

        public SwoaManager(SwoaDb swoaDb)
        {
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));

            ICelestialObjectCollection celestialObjects = new CelestialObjectList();
            CelestialObjectManager = new CelestialObjectManager(celestialObjects, swoaDb, dateTimeService);

            //CelestialObjectManager.Update();
        }

        #endregion

        #region Fields

        private readonly SwoaDb swoaDb;

        private readonly IDateTimeService dateTimeService = new BaseDateTimeService();

        #endregion

        #region Properties

        public CelestialObjectManager CelestialObjectManager { get; }

        #endregion

        #region Methods

        #endregion

    }
}
