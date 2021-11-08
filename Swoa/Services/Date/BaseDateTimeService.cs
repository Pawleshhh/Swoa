using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa
{
    public class BaseDateTimeService : IDateTimeService
    {
        public DateTime GetLocalDateTime()
            => DateTime.Now;
    }
}
