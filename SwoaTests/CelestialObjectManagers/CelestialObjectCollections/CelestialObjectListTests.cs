using CelestialObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Swoa.Tests
{
    [TestClass]
    public class CelestialObjectListTests : InterfaceCelestialObjectCollectionTests
    {

        #region Tests

        [TestMethod]
        public void CelestialObjectList_InitializesWithList_Passes()
        {
            new CelestialObjectList(new List<CelestialObject>());
        }

        [TestMethod]
        public void CelestialObjectList_InitalizesWithNullList_Throws()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CelestialObjectList(null));
        }

        [TestMethod]
        public void GetIndex_GetsCelestialObjectAtSpecifiedIndex_ReturnsCelestialObject()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));
            int index = 2;

            var result = celestialObjList[index];

            Assert.AreSame(arrayOfCelestialObjs[index], result);
        }

        [TestMethod]
        public void SetIndex_SetsCelestialObjectAtSpecifiedIndex_SetsCelestialObject()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var newCelestialObj = GetCelestialObject(4);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));
            int index = 1;

            celestialObjList[index] = newCelestialObj;

            Assert.AreSame(newCelestialObj, celestialObjList[index]);
        }

        [TestMethod]
        public void GetIndex_GetsCelestialObjectAtOutOfRangeIndex_ThrowsException()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => celestialObjList[100]);
        }

        [TestMethod]
        public void SetIndex_SetsCelestialObjectAtOutOfRangeIndex_ThrowsException()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => celestialObjList[100] = GetCelestialObject(1));
        }

        [TestMethod]
        public void Insert_InsertsItem_AddedEventRises()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var inserted = GetCelestialObject(4);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));

            EventAssert.EventRises<CelestialObjectCollectionChangedEventArgs>(e => celestialObjList.Added += e, () => celestialObjList.Insert(1, inserted));
        }

        [TestMethod]
        public void Insert_InsertsItem_AddedEventArgsAreAsExpected()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var inserted = GetCelestialObject(4);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));

            EventAssert.EventArgsEqual<CelestialObjectCollectionChangedEventArgs>(e => celestialObjList.Added += e, () => celestialObjList.Insert(1, inserted),
                e => e.ItemsChanged.Contains(inserted));
        }

        [TestMethod]
        public void Insert_InsertsNullItem_AddedEventDoesNotRise()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));

            EventAssert.EventDoesNotRise<CelestialObjectCollectionChangedEventArgs>(e => celestialObjList.Added += e, () => celestialObjList.Insert(1, null));
        }

        [TestMethod]
        public void Insert_InsertsAtIndexOutOfRange_ThrowsException()
        {
            var celestialObjList = new CelestialObjectList();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => celestialObjList.Insert(10, GetCelestialObject(1)));
        }

        [TestMethod]
        public void RemoveAt_RemovesAtSpecifiedIndex_RemovedEventRises()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var removed = GetCelestialObject(4);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));

            EventAssert.EventRises<CelestialObjectCollectionChangedEventArgs>(e => celestialObjList.Removed += e, () => celestialObjList.RemoveAt(1));
        }

        [TestMethod]
        public void RemoveAt_RemovesAtSpecifiedIndex_RemovedEventArgsAreAsExpected()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var removed = arrayOfCelestialObjs[1];
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));

            EventAssert.EventArgsEqual<CelestialObjectCollectionChangedEventArgs>(e => celestialObjList.Removed += e, () => celestialObjList.RemoveAt(1),
                e => e.ItemsChanged.Contains(removed));
        }

        [TestMethod]
        public void RemoveAt_RemovesAtIndexOutOfRange_ThrowsException()
        {
            var celestialObjList = new CelestialObjectList();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => celestialObjList.RemoveAt(10));
        }

        [TestMethod]
        public void IndexOf_GetsIndexOfItem_ReturnsIndex()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));
            int index = 1;

            int result = celestialObjList.IndexOf(arrayOfCelestialObjs[1]);

            Assert.AreEqual(index, result);
        }

        [TestMethod]
        public void IndexOf_GetsIndexOfItemThatIsNotInCollection_ReturnsNegativeIndex()
        {
            var arrayOfCelestialObjs = GetCelestialObjectArray(3);
            var celestialObjList = new CelestialObjectList(new List<CelestialObject>(arrayOfCelestialObjs));
            int index = -1;

            int result = celestialObjList.IndexOf(GetCelestialObject(10));

            Assert.AreEqual(index, result);
        }

        #endregion

        #region Helpers

        protected override ICelestialObjectCollection GetCelestialObjectCollection()
        {
            return new CelestialObjectList();
        }

        protected override ICelestialObjectCollection GetCelestialObjectCollection(params CelestialObject[] celestialObjects)
        {
            return new CelestialObjectList(new List<CelestialObject>(celestialObjects));
        }

        protected override ICelestialObjectCollection GetCelestialObjectCollection(ICollection<CelestialObject> celestialObjects)
        {
            return new CelestialObjectList(new List<CelestialObject>(celestialObjects));
        }

        #endregion

    }
}
