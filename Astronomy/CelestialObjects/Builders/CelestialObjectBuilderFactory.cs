using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public abstract class CelestialObjectBuilderFactory<T> : ICelestialObjectBuilderFactory
        where T : CelestialObjectType
    {

        public abstract CelestialObjectBuilder CreateCelestialObjectBuilder(T celestialObjectType);

        CelestialObjectBuilder ICelestialObjectBuilderFactory.CreateCelestialObjectBuilder(CelestialObjectType celestialObjectType)
            => CreateCelestialObjectBuilder((T)celestialObjectType);

    }
}
