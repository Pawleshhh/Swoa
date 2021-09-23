using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects.Properties
{
    public class TimeProperty : PropertyBase<TimeSpan>
    {

        public TimeProperty(string name, TimeSpan time)
            : base(name, time)
        {

        }

    }
}
