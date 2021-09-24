using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public interface ICelestialObjectBuilderFactory
    {

        CelestialObjectBuilder CreateCelestialObjectBuilder(CelestialObjectType celestialObjectType);

    }
}
