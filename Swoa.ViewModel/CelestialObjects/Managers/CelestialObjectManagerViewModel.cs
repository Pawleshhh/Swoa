using Astronomy;
using Astronomy.Units;
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

            celestialObjects = new ObservableCollection<CelestialObjectViewModel>(celestialObjectManager.CelestialObjects.Select(n => GetCelestialObjectVM(n)));
            CelestialObjects = new ReadOnlyObservableCollection<CelestialObjectViewModel>(celestialObjects);

            celestialObjectManager.Added += CelestialObjectManager_Added;
            celestialObjectManager.Removed += CelestialObjectManager_Removed;
            celestialObjectManager.Cleared += CelestialObjectManager_Cleared;
        }

        #endregion

        #region Fields

        private readonly CelestialObjectManager celestialObjectManager;
        private readonly ObservableCollection<CelestialObjectViewModel> celestialObjects;

        //private double mapWidth;
        //private double mapHeight;
        private double mapDiameter = 360.0;

        #endregion

        #region Properties

        public ReadOnlyObservableCollection<CelestialObjectViewModel> CelestialObjects { get; }

        public double MapDiameter
        {
            get => mapDiameter;
            set => SetProperty(() => mapDiameter == value, () => mapDiameter = value);
        }

        //public double MapWidth
        //{
        //    get => mapWidth;
        //    set => SetProperty(() => mapWidth == value, () => mapWidth = value);
        //}

        //public double MapHeight
        //{
        //    get => mapHeight;
        //    set => SetProperty(() => mapHeight == value, () => mapHeight = value);
        //}

        #endregion

        #region Methods

        private void CelestialObjectManager_Added(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            foreach (var item in e.ItemsChanged)
            {
                var celestialObjectVM = GetCelestialObjectVM(item);
                celestialObjects.Add(celestialObjectVM);
                celestialObjectVM.UpdatePosition();
            }
        }

        private void CelestialObjectManager_Removed(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            foreach (var item in e.ItemsChanged)
                celestialObjects.Remove(GetCelestialObjectVM(item));
        }
        private void CelestialObjectManager_Cleared(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            celestialObjects.Clear();
        }

        protected virtual CelestialObjectViewModel GetCelestialObjectVM(CelestialObject celestialObject)
        {
            var obj = new CelestialObjectViewModel(celestialObject, mapDiameter);
            obj.UpdatePosition();

            return obj;
        }

        #endregion

    }
}
