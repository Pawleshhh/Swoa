using CelestialObjects;
using System;
using System.Collections.Generic;

namespace Swoa.CelestialObjectManagers
{
    public class CelestialObjectManager
    {

        #region Constructors
        public CelestialObjectManager(ICelestialObjectCollection celestialObjects)
        {
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
        }
        #endregion

        #region Fields

        private readonly ICelestialObjectCollection celestialObjects;

        #endregion

        #region Properties

        public IReadOnlyCollection<CelestialObject> CelestialObjects => celestialObjects;

        #endregion

        #region Events

        //public event EventHandler<CelestialObjectCollectionChangedEventArgs> Added
        //{
        //    add => celestialObjects.Added += value;
        //    remove => celestialObjects.Added -= value;
        //}
        //public event EventHandler<CelestialObjectCollectionChangedEventArgs> Removed
        //{
        //    add => celestialObjects.Removed += value;
        //    remove => celestialObjects.Removed -= value;
        //}
        //public event EventHandler<CelestialObjectCollectionChangedEventArgs> Cleared
        //{
        //    add => celestialObjects.Cleared += value;
        //    remove => celestialObjects.Cleared -= value;
        //}

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

        #endregion

    }
}
