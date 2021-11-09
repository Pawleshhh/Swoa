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
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace Swoa.ViewModel
{
    public class CelestialObjectManagerViewModel : NotifyPropertyChanges, IAsyncTaskDirector
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

        private bool isWorking;
        private CancellationTokenSource tokenSource;
        private Task updatePositionTask;

        private CelestialObjectViewModel selectedObject;

        #endregion

        #region Properties

        public TimeMachineViewModel TimeMachineVM { get; }

        public ReadOnlyObservableCollection<CelestialObjectViewModel> CelestialObjects { get; }

        public CelestialObjectViewModel SelectedObject
        {
            get => selectedObject;
            private set => SetProperty(ref selectedObject, value);
        }

        public double MapDiameter
        {
            get => mapDiameter;
            set
            {
                if (SetProperty(ref mapDiameter, value))
                    UpdateCelestialObjectsPositionAsync();
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
            set => SetProperty(ref areDirectionsVisible, value);
        }

        public bool IsWorking
        {
            get => isWorking;
            set => SetProperty(ref isWorking, value);
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

        public async void UpdateCelestialObjectsPositionAsync()
        {
            if (updatePositionTask != null && updatePositionTask.Status == TaskStatus.Running)
                CancelTask();

            TimeMachineVM.WaitForTask();

            IsWorking = true;

            tokenSource = new CancellationTokenSource();
            var ct = tokenSource.Token;

            updatePositionTask = Task.Run(() =>
            {
                foreach (var celestialObject in celestialObjects)
                {
                    SetPosition(celestialObject);

                    if (ct.IsCancellationRequested)
                        break;
                }
            }, ct);
            await updatePositionTask;

            IsWorking = false;
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
                    Subscribe(celestialObjectVM);

                    celestialObjects.Add(celestialObjectVM);

                    SetPosition(celestialObjectVM);
                }
            }
        }

        private void CelestialObjectManager_Removed(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            lock (itemsLock)
            {
                foreach (var index in e.Indexes)
                {
                    Unsubscribe(celestialObjects[index]);
                    celestialObjects.RemoveAt(index);
                }
            }
        }

        private void CelestialObjectManager_Cleared(object sender, CelestialObjectCollectionChangedEventArgs e)
        {
            lock (itemsLock)
            {
                foreach (var item in celestialObjects)
                    Unsubscribe(item);

                celestialObjects.Clear();
            }
        }

        private void CelestialObjectVM_HorizonCoordsChanged(object sender, Utilities.DataChangedEventArgs<HorizonCoordinates> e)
        {
            SetPosition((CelestialObjectViewModel)sender);
        }

        private void CelestialObjectVM_Selected(object sender, EventArgs e)
        {
            SelectedObject = (CelestialObjectViewModel)sender;
        }

        protected virtual CelestialObjectViewModel GetCelestialObjectVM(CelestialObject celestialObject)
        {
            var obj = new CelestialObjectViewModel(celestialObject);

            return obj;
        }

        protected void Subscribe(CelestialObjectViewModel celestialObjectVM)
        {
            celestialObjectVM.HorizonCoordsChanged += CelestialObjectVM_HorizonCoordsChanged;
            celestialObjectVM.Selected += CelestialObjectVM_Selected;
        }

        protected void Unsubscribe(CelestialObjectViewModel celestialObjectVM)
        {
            celestialObjectVM.HorizonCoordsChanged -= CelestialObjectVM_HorizonCoordsChanged;
            celestialObjectVM.Selected -= CelestialObjectVM_Selected;
        }

        public void CancelTask()
        {
            tokenSource?.Cancel();
            WaitForTask();
            IsWorking = false;
        }

        public void WaitForTask()
        {
            updatePositionTask?.Wait();
        }

        public TaskStatus GetTaskStatus()
        {
            return updatePositionTask?.Status ?? TaskStatus.RanToCompletion;
        }

        #endregion

    }
}
