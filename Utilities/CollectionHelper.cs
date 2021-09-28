using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class CollectionHelper
    {

        public static bool CollectionsEqual<T>(ICollection<T> collection1, ICollection<T> collection2)
        {
            int count1 = collection1.Count();
            int count2 = collection2.Count();

            if (count1 != count2)
                return false;

            int i = 0;

            return collection1.All(n => n.Equals(collection2.ElementAt(i++)));
        }

    }
}
