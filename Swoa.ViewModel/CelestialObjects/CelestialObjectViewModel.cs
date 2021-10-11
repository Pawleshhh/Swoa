using CelestialObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class CelestialObjectViewModel : NotifyPropertyChanges
    {

        public CelestialObjectViewModel(CelestialObject celestialObject)
        {
            CelestialObject = celestialObject;

            (XPos, YPos) = GetCartesianCoords();
        }

        public CelestialObject CelestialObject { get; init; }

        private double x;
        private double y;

        public double XPos
        {
            get => x;
            set => SetProperty(() => x == value, () => x = value);
        }
        public double YPos
        {
            get => y;
            set => SetProperty(() => y == value, () => y = value);
        }

        private (double, double) GetCartesianCoords()
        {
            var (alt, az) = (CelestialObject.HorizontalCoordinates.Altitude, CelestialObject.HorizontalCoordinates.Azimuth);

            double r = (90.0 - alt) / 90.0 * 180.0;

            var x = r * Math.Cos(ToRadian(alt)) - 2.5;
            var y = r * Math.Sin(ToRadian(az)) - 2.5;

            return (x, y);
        }

        private double ToRadian(double deg)
            => deg * Math.PI / 180.0;

    }
}
