using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialObjects
{
    public interface ICelestialObjectBuilder
    {

        CelestialObject Build();

        Dictionary<string, IProperty> GetProperties();

    }
}
