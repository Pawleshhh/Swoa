using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public abstract class CelestialObjectInfoViewModelCollectionBuilder
    {

        public abstract ObservableCollection<ICelestialObjectInfoViewModel> BuildCelestialObjectInfoViewModelCollection(CelestialObjectViewModel celestialObjectVM);

        protected ICelestialObjectInfoViewModel<T> GetInfoViewModel<T>(string name, T value, string format = null)
            => new CelestialObjectInfoViewModel<T>(name, value) { Format = format };

    }
}
