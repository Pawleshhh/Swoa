using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialObjects
{
    public interface ICelestialObject : IEquatable<ICelestialObject>
    {

        string Name { get; }

        double VisualMagnitude { get; }

    }
}
