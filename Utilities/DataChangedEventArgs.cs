using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class DataChangedEventArgs<T> : EventArgs
    {

        public DataChangedEventArgs(T previous, T current)
        {
            Previous = previous;
            Current = current;
        }

        public T Previous { get; }
        public T Current { get; }

    }
}
