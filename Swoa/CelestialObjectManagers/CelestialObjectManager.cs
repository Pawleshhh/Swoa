using Astronomy;
using CelestialObjects;
using SwoaDatabaseAPI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using static Utilities.PropertyChangedHelper;

namespace Swoa
{
    public class CelestialObjectManager
    {

        #region Constructors
        public CelestialObjectManager(ICelestialObjectCollection celestialObjects, SwoaDb swoaDb)
        {
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));

            TimeMachine = new TimeMachine(swoaDb);

            TimeMachine.DateChanged += TimeMachine_DateChanged;
            TimeMachine.LongitudeChanged += TimeMachine_LongitudeChanged;
            TimeMachine.LatitudeChanged += TimeMachine_LatitudeChanged;
        }

        #endregion

        #region Fields

        private readonly ICelestialObjectCollection celestialObjects;
        private readonly SwoaDb swoaDb;

        private readonly bool asyncUpdate = true;
        private bool isWorking;

        private readonly CelestialObjectReviewer mainReviewer = new CelestialObjectReviewer();
        //private readonly CelestialObjectReviewer customReviewer = new CelestialObjectReviewer();

        private CancellationTokenSource? tokenSource;

        #endregion

        #region Properties

        public IReadOnlyCollection<CelestialObject> CelestialObjects => celestialObjects;

        public TimeMachine TimeMachine { get; }

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

        public event EventHandler<DataChangedEventArgs<bool>>? IsWorkingChanged;

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

        public void Update()
        {
            Clear();
            foreach (var item in TimeMachine.GetCurrentMap())
            {
                Add(item);
            }
        }

        public async void UpdateAsync()
        {
            if (IsWorking)
                CancelWork();
            else
                IsWorking = true;

            tokenSource = new CancellationTokenSource();
            var ct = tokenSource.Token;

            try
            {
                await Task.Run(() =>
                {
                    ct.ThrowIfCancellationRequested();

                    Clear();
                    var map = TimeMachine.GetCurrentMap(() => ct.IsCancellationRequested);

                    foreach(var obj in map)
                    {
                        Add(obj);
                        if (ct.IsCancellationRequested)
                            break;
                    }
                }, tokenSource.Token);
            }
            finally
            {
                IsWorking = false;
            }
        }

        public void CancelWork()
        {
            tokenSource?.Cancel();
            IsWorking = false;
        }

        private void TimeMachine_LatitudeChanged(object? sender, Utilities.DataChangedEventArgs<double> e)
        {
            if (asyncUpdate)
                UpdateAsync();
            else
                Update();
        }

        private void TimeMachine_LongitudeChanged(object? sender, Utilities.DataChangedEventArgs<double> e)
        {
            if (asyncUpdate)
                UpdateAsync();
            else
                Update();
        }

        private void TimeMachine_DateChanged(object? sender, Utilities.DataChangedEventArgs<DateTime> e)
        {
            if (asyncUpdate)
                UpdateAsync();
            else
                Update();
        }

        #endregion

    }
}
