using Swoa;

namespace Swoa
{
    public class SwoaManager
    {

        #region Constructors

        public SwoaManager()
        {
            ICelestialObjectCollection celestialObjects = new CelestialObjectList();

            CelestialObjectManager = new CelestialObjectManager(celestialObjects);
        }

        #endregion

        #region Properties

        public CelestialObjectManager CelestialObjectManager { get; }

        #endregion

    }
}
