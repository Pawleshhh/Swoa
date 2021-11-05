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
        public CelestialObjectCollectionChangedEventArgs(ICollection<CelestialObject> itemsChanged, int[] indexes)
        {
            ItemsChanged = itemsChanged ?? throw new ArgumentNullException(nameof(itemsChanged));
            Indexes = indexes ?? throw new ArgumentNullException(nameof(indexes));
        }
        #endregion

        #region Properties

        public ICollection<CelestialObject> ItemsChanged { get; }
        public ICollection<int> Indexes { get; }

        #endregion

    }
}
