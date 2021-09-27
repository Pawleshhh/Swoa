using CelestialObjects;
using System;
using System.Collections.Generic;

namespace Swoa.CelestialObjectManagers
{
    public class CelestialObjectManager
    {

        #region Constructors
        public CelestialObjectManager()
        {
            celestialObjects = new List<CelestialObject>();
        }

        public CelestialObjectManager(List<CelestialObject> celestialObjects)
        {
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
        }
        #endregion

        #region Fields

        private readonly List<CelestialObject> celestialObjects;

        #endregion

        #region Properties



        #endregion

        #region Methods

        #endregion

    }
}
