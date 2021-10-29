using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwoaDatabaseAPI
{
    public static class SwoaDbHelper
    {

        public static T GetDataOrDefault<T>(DbDataReader reader, Func<int, T> getData, int column)
        {
            if (reader.IsDBNull(column))
                return default;
            else
                return getData(column);
        }

    }
}
