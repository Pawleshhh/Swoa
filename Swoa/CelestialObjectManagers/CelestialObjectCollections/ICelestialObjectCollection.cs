using CelestialObjects;
using System;
using System.Collections.Generic;

namespace Swoa
{
    public interface ICelestialObjectCollection : ICollection<CelestialObject>
    {

        #region Events

        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Added;
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Removed;
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Cleared;

        #endregion

    }
}
