using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects.Properties
{
    public interface IProperty : IEquatable<IProperty>
    {

        string Name { get; }

        object Value { get; }

    }

}
