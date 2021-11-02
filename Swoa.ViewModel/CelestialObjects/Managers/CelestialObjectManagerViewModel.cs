using Astronomy;
using Astronomy.Units;
using CelestialObjects;
using Swoa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class CelestialObjectManagerViewModel : NotifyPropertyChanges
    {

        #region Constructors

        public CelestialObjectManagerViewModel(CelestialObjectManager celestialObjectManager, IUiThread uiThread)
        {
            this.celestialObjectManager = celestialObjectManager ?? throw new ArgumentNullException(nameof(celestialObjectManager));
            this.uiThread = uiThread ?? throw new ArgumentNullException(nameof(uiThread));

            celestialObjects = new ObservableCollection<CelestialObjectViewModel>(celestialObjectManager.CelestialObjects.Select(n => GetCelestialObjectVM(n)));
            CelestialObjects = new ReadOnlyObservableCollection<CelestialObjectViewModel>(celestialObjects);

            TimeMachineVM = new TimeMachineViewModel(celestialObjectManager.TimeMachine);

            celestialObjectManager.Added += CelestialObjectManager_Added;
            celestialObjectManager.Removed += CelestialObjectManager_Removed;
            celestialObjectManager.Cleared += CelestialObjectManager_Cleared;

            itemsLock = uiThread.OnUiThread(CelestialObjects);
        }

        #endregion

        #region Fields

        private readonly object itemsLock;
        private readonly IUiThread uiThread;

        private readonly CelestialObjectManager celestialObjectManager;
        private readonly ObservableCollection<CelestialObjectViewModel> celestialObjects;

        //private double mapWidth;
        //private double mapHeight;
        private double mapDiameter;

        private bool isAzGridVisible = true;
        private bool isAltGridVisible = true;
        private bool areDirectionsVisible = true;

        #endregion

        #region Properties

        public TimeMachineViewModel TimeMachineVM { get; }

        public ReadOnlyObservableCollection<CelestialObjectViewModel> CelestialObjects { get; }

        public double MapDiameter
        {
            get => mapDiameter;
            set
            {
                if (SetProperty(ref mapDiameter, value))
                    UpdateCelestialObjects();
            }
        }

        public bool IsAzGridVisible
        {
            get => isAzGridVisible;
            set => SetProperty(ref isAzGridVisible, value);
        }

        public bool IsAltGridVisible
        {
            get => isAltGridVisible;
            set => SetProperty(ref isAltGridVisible, value);
        }

        public bool AreDirectionsVisible
        {
            get => areDirectionsVisible;
            set => SetProperty(ref areDirectionsVisible, areDirectionsVisible);
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

        public void UpdateCelestialObjects()
        {
            foreach(var celestialObject in celestialObjects)
            {
                SetPosition(celestialObject);
            }
        }

        private void SetPosition(CelestialObjectViewModel celestialObjectVM)
        {
            var (alt, az) = celestialObjectVM.CelestialObject.HorizontalCoordinates;
            var (width, height) = (celestialObjectVM.Width, celestialObjectVM.Height);
            const double MAXALT = HorizonCoordinates.MAXALTITUDE;
            double r = (MAXALT - alt) / MAXALT * (mapDiameter / 2.0);

            var x = r * MathHelper.CosD(az) - (width / 2.0);
            var y = r * MathHelper.SinD(az) - (height / 2.0);

            celestialObjectVM.XPos = x;
            celestialObjectVM.YPos = y;
        }

        private void CelestialObjectManager_Added(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            lock (itemsLock)
            {
                foreach (var item in e.ItemsChanged)
                {
                    var celestialObjectVM = GetCelestialObjectVM(item);
                    celestialObjects.Add(celestialObjectVM);

                    SetPosition(celestialObjectVM);

                    //celestialObjectVM.WhenPropertyChanged.Subscribe(n =>
                    //{
                    //    if (n.Equals(nameof(celestialObjectVM.Width)) ||
                    //            n.Equals(nameof(celestialObjectVM.Height)))
                    //        ;
                    //});
                }
            }
        }

        private void CelestialObjectManager_Removed(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            lock (itemsLock)
            {
                foreach (var item in e.ItemsChanged)
                    celestialObjects.Remove(GetCelestialObjectVM(item));
            }
        }

        private void CelestialObjectManager_Cleared(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            lock (itemsLock)
            {
                celestialObjects.Clear();
            }
        }

        protected virtual CelestialObjectViewModel GetCelestialObjectVM(CelestialObject celestialObject)
        {
            var obj = new CelestialObjectViewModel(celestialObject);

            return obj;
        }

        #endregion

    }
}
