using Astronomy;
using CelestialObjects;
using SwoaDatabaseAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using static Utilities.PropertyChangedHelper;

namespace Swoa
{
    public class CelestialObjectManager
    {

        #region Constructors
        public CelestialObjectManager(ICelestialObjectCollection celestialObjects, SwoaDb swoaDb, IDateTimeService dateTimeService)
        {
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));
            this.dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));

            TimeMachine = new TimeMachine(dateTimeService);
            CelestialObjectsLoader = new CelestialObjectsLoader(swoaDb, TimeMachine, celestialObjects);

            TimeMachine.DateChanged += TimeMachine_DateChanged;
            TimeMachine.LongitudeChanged += TimeMachine_LongitudeChanged;
            TimeMachine.LatitudeChanged += TimeMachine_LatitudeChanged;
            TimeMachine.SetCurrentDateChanged += TimeMachine_SetCurrentDateChanged;
            TimeMachine.MagnitudeChanged += TimeMachine_MagnitudeChanged;
        }

        #endregion

        #region Fields

        private readonly ICelestialObjectCollection celestialObjects;
        private readonly SwoaDb swoaDb;
        private readonly IDateTimeService dateTimeService;

        private bool changeOnUpdateEnabled = true;
        private bool isWorking;
        #endregion

        #region Properties

        public IReadOnlyCollection<CelestialObject> CelestialObjects => celestialObjects;

        public TimeMachine TimeMachine { get; }
        public CelestialObjectsLoader CelestialObjectsLoader { get; }

        public bool ChangeOnUpdateEnabled
        {
            get => changeOnUpdateEnabled;
            set => SetProperty(ref changeOnUpdateEnabled, value, OnChangeOnUpdateEnabledChanged);
        }

        public bool IsWorking
        {
            get => isWorking;
            set => SetProperty(ref isWorking, value, OnIsWorkingChanged);
        }

        #endregion

        #region Events

        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Added
        {
            add => celestialObjects.Added += value;
            remove => celestialObjects.Added -= value;
        }
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Removed
        {
            add => celestialObjects.Removed += value;
            remove => celestialObjects.Removed -= value;
        }
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Cleared
        {
            add => celestialObjects.Cleared += value;
            remove => celestialObjects.Cleared -= value;
        }

        public event EventHandler<DataChangedEventArgs<bool>>? ChangeOnUpdateEnabledChanged;
        public event EventHandler<DataChangedEventArgs<bool>>? IsWorkingChanged;

        protected void OnChangeOnUpdateEnabledChanged(bool prev, bool curr)
            => ChangeOnUpdateEnabledChanged?.Invoke(this, new DataChangedEventArgs<bool>(prev, curr));
        protected void OnIsWorkingChanged(bool previous, bool current)
            => IsWorkingChanged?.Invoke(this, new DataChangedEventArgs<bool>(previous, current));

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

        private void OnAnyPropertyChanged(bool wait = false)
        {
            if (ChangeOnUpdateEnabled)
            {
                if (wait)
                    CelestialObjectsLoader.WaitForTask();

                CelestialObjectsLoader.UpdateCurrentMapAsync();
            }
        }

        private void TimeMachine_LatitudeChanged(object? sender, Utilities.DataChangedEventArgs<double> e)
        {
            OnAnyPropertyChanged();
        }

        private void TimeMachine_LongitudeChanged(object? sender, Utilities.DataChangedEventArgs<double> e)
        {
            OnAnyPropertyChanged();
        }

        private void TimeMachine_DateChanged(object? sender, Utilities.DataChangedEventArgs<DateTime> e)
        {
            OnAnyPropertyChanged();
        }

        private void TimeMachine_SetCurrentDateChanged(object? sender, DataChangedEventArgs<DateTime> e)
        {
            OnAnyPropertyChanged(true);
        }

        private void TimeMachine_MagnitudeChanged(object? sender, DataChangedEventArgs<double> e)
        {
            OnAnyPropertyChanged();
        }

        #endregion

    }
}
