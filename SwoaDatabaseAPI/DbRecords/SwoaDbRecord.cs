using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwoaDatabaseAPI
{
    public abstract record SwoaDbRecord
    {
        public long Id { get; init; }
        public string Proper { get; init; }

        public double Ra { get; init; }
        public double Dec { get; init; }
        public double SunDist { get; init; }
        public double EarthDist { get; init; }
        public double Mag { get; init; }
    }
}
