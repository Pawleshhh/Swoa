using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwoaDatabaseAPI
{
    public record SwoaDbStarRecord : SwoaDbRecord
    {
        public string Hip { get; init; }
        public string Hd { get; init; }
        public string Hr { get; init; }
        public string Gl { get; init; }
        public string Bf { get; init; }

        public double AbsMag { get; init; }

        public string Spect { get; init; }
        public string Con { get; init; }

        public double LengthOfDay { get; }
    }
}
