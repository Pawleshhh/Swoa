using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects.Properties
{
    public class DateProperty : PropertyBase<DateTime>
    {

        public DateProperty(string name, DateTime dateTime)
            : base(name, dateTime)
        {

        }

    }
}
