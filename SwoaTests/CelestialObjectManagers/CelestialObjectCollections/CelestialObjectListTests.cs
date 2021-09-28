using CelestialObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.Tests
{
    [TestClass]
    public class CelestialObjectListTests : InterfaceCelestialObjectCollectionTests
    {

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
