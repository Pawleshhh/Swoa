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

        protected ICelestialObjectInfoViewModel<T> GetInfoViewModel<T>(string name, T value)
            => new CelestialObjectInfoViewModel<T>(name, value);

    }
}
