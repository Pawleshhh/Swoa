using CelestialObjects;
using Swoa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class CelestialObjectManagerViewModel : NotifyPropertyChanges
    {

        #region Constructors

        public CelestialObjectManagerViewModel(CelestialObjectManager celestialObjectManager)
        {
            this.celestialObjectManager = celestialObjectManager ?? throw new ArgumentNullException(nameof(celestialObjectManager));

            celestialObjects = new ObservableCollection<CelestialObject>(celestialObjectManager.CelestialObjects);
            CelestialObjects = new ReadOnlyObservableCollection<CelestialObject>(celestialObjects);

            celestialObjectManager.Added += CelestialObjectManager_Added;
            celestialObjectManager.Removed += CelestialObjectManager_Removed;
            celestialObjectManager.Cleared += CelestialObjectManager_Cleared;
        }

        #endregion

        #region Fields

        private readonly CelestialObjectManager celestialObjectManager;
        private readonly ObservableCollection<CelestialObject> celestialObjects;

        #endregion

        #region Properties

        public ReadOnlyObservableCollection<CelestialObject> CelestialObjects { get; }

        #endregion

        #region Methods

        private void CelestialObjectManager_Added(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            foreach(var item in e.ItemsChanged)
                celestialObjects.Add(item);
        }

        private void CelestialObjectManager_Removed(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            foreach (var item in e.ItemsChanged)
                celestialObjects.Remove(item);
        }
        private void CelestialObjectManager_Cleared(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            celestialObjects.Clear();
        }

        #endregion

    }
}
