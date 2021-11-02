using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public interface IUiThread
    {

        object OnUiThread(object param);

    }
}
