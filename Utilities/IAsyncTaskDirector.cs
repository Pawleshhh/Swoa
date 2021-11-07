using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface IAsyncTaskDirector
    {

        void CancelTask();

        void WaitForTask();

        TaskStatus GetTaskStatus();

    }
}
