using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public abstract class CelestialObjectInfoViewModelFactory
    {

        public abstract ObservableCollection<ICelestialObjectInfoViewModel> CreateCelestialObjectInfoViewModels(CelestialObjectViewModel celestialObjectVM);

        protected ICelestialObjectInfoViewModel<T> GetInfoViewModel<T>(string name, T value)
            => new CelestialObjectInfoViewModel<T>(name, value);

    }
}
