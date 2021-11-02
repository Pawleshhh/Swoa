using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class SwoaManagerViewModel : NotifyPropertyChanges
    {

        #region Constructors
        public SwoaManagerViewModel(SwoaManager swoaManager, IUiThread uiThread)
        {
            this.swoaManager = swoaManager ?? throw new ArgumentNullException(nameof(swoaManager));

            CelestialObjectManagerVM = new CelestialObjectManagerViewModel(swoaManager.CelestialObjectManager, uiThread);
        }
        #endregion

        #region Fields

        private readonly SwoaManager swoaManager;

        #endregion

        #region Properties

        public CelestialObjectManagerViewModel CelestialObjectManagerVM { get; }

        #endregion


    }
}
