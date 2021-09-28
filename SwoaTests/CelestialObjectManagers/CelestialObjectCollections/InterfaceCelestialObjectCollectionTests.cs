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
        public void Add_AddsItem_CollectionContainsAddedItem()
        {
            var celestialObjCollection = GetCelestialObjectCollection();
            var added = GetCelestialObject(1);

            celestialObjCollection.Add(added);

            CollectionAssert.Contains(celestialObjCollection.ToArray(), added);
        }

        [TestMethod]
        public void Remove_RemovesItem_CollectionDoesNotContainRemovedItem()
        {
            var removed = GetCelestialObject(1);
            var celestialObjCollection = GetCelestialObjectCollection(removed);

            celestialObjCollection.Remove(removed);

            CollectionAssert.DoesNotContain(celestialObjCollection.ToArray(), removed);
        }

        [TestMethod]
        public void Clear_ClearsCollection_CollectionIsEmpty()
        {
            var cleared = GetCelestialObjectArray(10);
            var celestialObjCollection = GetCelestialObjectCollection(cleared);

            celestialObjCollection.Clear();

            Assert.AreEqual(0, celestialObjCollection.Count);
        }

        [TestMethod]
        public void Count_GetsCount_CountIsEqualToNumberOfItems()
        {
            int count = 10;
            var items = GetCelestialObjectArray(count);
            var celestialObjCollection = GetCelestialObjectCollection(items);

            int result = celestialObjCollection.Count;

            Assert.AreEqual(count, result);
        }

        [TestMethod]
        public void Contains_CheckIfAddedItemExists_ReturnsTrue()
        {
            var item = GetCelestialObject(1);
            var celestialObjCollection = GetCelestialObjectCollection(item);

            bool result = celestialObjCollection.Contains(item);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Contains_CheckIfNotAddedItemExists_ReturnsFalse()
        {
            var item = GetCelestialObject(1);
            var celestialObjCollection = GetCelestialObjectCollection();

            bool result = celestialObjCollection.Contains(item);

            Assert.IsFalse(result);
        }

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
            var celestialObjCollection = GetCelestialObjectCollection(new CelestialObject[] { celestialObject });

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