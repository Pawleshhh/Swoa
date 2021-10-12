using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Astronomy.MathHelper;

namespace Astronomy
{
    public static class CoordinatesConverter
    {

        public static (double, double) HorizonToCartesianCoords(double r, HorizonCoordinates horizonCoords)
        {
            var az = horizonCoords.Azimuth;

            var x = r * CosD(az);
            var y = r * CosD(az);

            return (x, y);
        }

    }
}
