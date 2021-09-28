using CelestialObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Swoa.Tests
{
    [TestClass()]
    public abstract class InterfaceCelestialObjectCollectionTests
    {

        #region Tests

        [TestMethod]
        public void Added_AddsItem_AddedEventRises()
        {
            var celestialObjCollection = GetCelestialObjectCollection();
            var celestialObject = GetCelestialObject(1);

            EventAssert.EventRises<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Added += e, 
                () => celestialObjCollection.Add(celestialObject));
        }

        [TestMethod]
        public void Added_AddsNullItem_AddedEventDoesNotRise()
        {
            var celestialObjCollection = GetCelestialObjectCollection();

            EventAssert.EventDoesNotRise<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Added += e,
                () => celestialObjCollection.Add(null));
        }

        [TestMethod]
        public void Added_AddsItem_AddedEventArgsAreAsExpected()
        {
            var celestialObject = GetCelestialObject(1);
            var expectedEventArgs = new CelestialObjectCollectionChangedEventArgs(new CelestialObject[] { celestialObject } );
            var celestialObjCollection = GetCelestialObjectCollection();

            EventAssert.EventArgsEqual<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Added += e,
                () => celestialObjCollection.Add(celestialObject), e => e.ItemsChanged.Contains(celestialObject));
        }

        [TestMethod]
        public void Removed_RemovesItem_RemovedEventRises()
        {
            var celestialObj = GetCelestialObject(1);
            var celestialObjCollection = GetCelestialObjectCollection(celestialObj);

            EventAssert.EventRises<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Removed += e,
                () => celestialObjCollection.Remove(celestialObj));
        }

        [TestMethod]
        public void Removed_RemovesItemThatCollectionDoesNotContain_RemovedEventDoesNotRise()
        {
            var added = GetCelestialObject(1);
            var notAdded = GetCelestialObject(2);
            var celestialObjCollection = GetCelestialObjectCollection(added);

            EventAssert.EventDoesNotRise<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Removed += e,
                () => celestialObjCollection.Remove(notAdded));
        }

        [TestMethod]
        public void Removed_RemovesNullItem_RemovedEventDoesNotRise()
        {
            var added = GetCelestialObject(1);
            var celestialObjCollection = GetCelestialObjectCollection(added);

            EventAssert.EventDoesNotRise<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Removed += e,
                () => celestialObjCollection.Remove(null));
        }

        [TestMethod]
        public void Removed_RemovesItem_RemovedEventArgsAreAsExpected()
        {
            var celestialObject = GetCelestialObject(1);
            var celestialObjCollection = GetCelestialObjectCollection();

            EventAssert.EventArgsEqual<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Removed += e,
                () => celestialObjCollection.Remove(celestialObject), e => e.ItemsChanged.Contains(celestialObject));
        }

        [TestMethod]
        public void Cleared_ClearsItems_ClearedEventArgsRises()
        {
            var celestialObjs = GetCelestialObjectArray(10);
            var celestialObjCollection = GetCelestialObjectCollection(celestialObjs);

            EventAssert.EventRises<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Cleared += e,
                () => celestialObjCollection.Clear());
        }

        [TestMethod]
        public void Cleared_ClearsItems_ClearedEventArgsAreAsExpected()
        {
            var celestialObjs = GetCelestialObjectArray(10);
            var celestialObjCollection = GetCelestialObjectCollection(celestialObjs);

            EventAssert.EventArgsEqual<CelestialObjectCollectionChangedEventArgs>(e => celestialObjCollection.Cleared += e,
                () => celestialObjCollection.Clear(), e => CollectionHelper.CollectionsEqual(e.ItemsChanged, celestialObjs));
        }

        #endregion

        #region Helpers

        protected virtual CelestialObject GetCelestialObject(int h)
        {
            return new CelestialObjectMock()
            {
                Name = h.ToString()
            };
        }

        protected virtual CelestialObject[] GetCelestialObjectArray(int count)
        {
            var celestialObjs = new CelestialObject[count];

            for (int i = 0; i < count; i++)
                celestialObjs[i] = GetCelestialObject(i + 1);

            return celestialObjs;
        }

        protected abstract ICelestialObjectCollection GetCelestialObjectCollection();
        protected abstract ICelestialObjectCollection GetCelestialObjectCollection(params CelestialObject[] celestialObjects);
        protected abstract ICelestialObjectCollection GetCelestialObjectCollection(ICollection<CelestialObject> celestialObjects);

        #endregion

    }
}