using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy.CelestialObjects
{
    public class PlanetObjectType : CelestialObjectType
    {
        protected PlanetObjectType(string name) : base(name)
        {
        }

        public static PlanetObjectType DefaultPlanet { get; } = new PlanetObjectType("Default planet");
        public static PlanetObjectType DwarfPlanet { get; } = new PlanetObjectType("Dwarf planet");
        public static PlanetObjectType PhasedPlanet { get; } = new PlanetObjectType("Phased planet");
        public static PlanetObjectType RingedPlanet { get; } = new PlanetObjectType("Ringed planet");

        public override CelestialObjectType GetDefault()
        {
            return DefaultPlanet;
        }

    }
}
