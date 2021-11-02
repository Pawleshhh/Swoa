using Swoa.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Swoa.UI
{
    public class WpfThread : IUiThread
    {

        private object _itemsLock;

        public object OnUiThread(object param)
        {
            _itemsLock = new object();
            BindingOperations.EnableCollectionSynchronization((IEnumerable)param, _itemsLock);

            return _itemsLock;
        }
    }
}
