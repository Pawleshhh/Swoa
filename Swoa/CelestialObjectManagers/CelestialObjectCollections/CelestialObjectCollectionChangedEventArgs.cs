using CelestialObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa
{
    public class CelestialObjectCollectionChangedEventArgs : EventArgs
    {

        #region Constructors
        public CelestialObjectCollectionChangedEventArgs(ICollection<CelestialObject> itemsChanged)
        {
            ItemsChanged = itemsChanged ?? throw new ArgumentNullException(nameof(itemsChanged));
        }
        #endregion

        #region Properties

        public ICollection<CelestialObject> ItemsChanged { get; }

        #endregion

    }
}
