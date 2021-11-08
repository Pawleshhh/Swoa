﻿using Astronomy;
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
        public CelestialObjectManager(ICelestialObjectCollection celestialObjects, SwoaDb swoaDb, IDateTimeService dateTimeService)
        {
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
            this.swoaDb = swoaDb ?? throw new ArgumentNullException(nameof(swoaDb));
            this.dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));

            TimeMachine = new TimeMachine(swoaDb, celestialObjects, dateTimeService);

            TimeMachine.DateChanged += TimeMachine_DateChanged;
            TimeMachine.LongitudeChanged += TimeMachine_LongitudeChanged;
            TimeMachine.LatitudeChanged += TimeMachine_LatitudeChanged;
        }

        #endregion

        #region Fields

        private readonly ICelestialObjectCollection celestialObjects;
        private readonly SwoaDb swoaDb;
        private readonly IDateTimeService dateTimeService;

        private readonly bool asyncUpdate = true;

        private readonly CelestialObjectReviewer mainReviewer = new CelestialObjectReviewer();

        //private readonly CelestialObjectReviewer customReviewer = new CelestialObjectReviewer();

        #endregion

        #region Properties

        public IReadOnlyCollection<CelestialObject> CelestialObjects => celestialObjects;

        public TimeMachine TimeMachine { get; }

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

        private void TimeMachine_LatitudeChanged(object? sender, Utilities.DataChangedEventArgs<double> e)
        {
            //if (asyncUpdate)
            //    UpdateAsync();
            //else
            //    Update();
            TimeMachine.UpdateCurrentMapAsync();
        }

        private void TimeMachine_LongitudeChanged(object? sender, Utilities.DataChangedEventArgs<double> e)
        {
            //if (asyncUpdate)
            //    UpdateAsync();
            //else
            //    Update();
            TimeMachine.UpdateCurrentMapAsync();
        }

        private void TimeMachine_DateChanged(object? sender, Utilities.DataChangedEventArgs<DateTime> e)
        {
            //if (asyncUpdate)
            //    UpdateAsync();
            //else
            //    Update();
            TimeMachine.UpdateCurrentMapAsync();
        }

        #endregion

    }
}
