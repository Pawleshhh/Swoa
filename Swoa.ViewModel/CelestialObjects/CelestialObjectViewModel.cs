using Astronomy;
using Astronomy.Units;
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

        /* Needed:
         * Alt-Az x
         * Radius/Diameter of SWOA Map
         * CelestialObject Width and Height x
         */

        public CelestialObjectViewModel(CelestialObject celestialObject, double mapDiameter)
        {
            CelestialObject = celestialObject;

            this.mapDiameter = mapDiameter;

            UpdatePosition();
        }

        public CelestialObject CelestialObject { get; init; }

        private double mapDiameter;

        private double x = 5;
        private double y = 5;

        private double height;
        private double width;

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

        public double Height
        {
            get => height;
            set
            {
                if (SetProperty(() => height == value, () => height = value))
                    UpdatePosition();
            }
        }

        public double Width
        {
            get => width;
            set
            {
                if (SetProperty(() => width == value, () => width = value))
                    UpdatePosition();
            }
        }

        public void UpdatePosition()
        {
            var (alt, az) = CelestialObject.HorizontalCoordinates;
            const double MAXALT = HorizonCoordinates.MAXALTITUDE;
            double r = (MAXALT - alt) / MAXALT * (mapDiameter / 2.0);

            x = r * MathHelper.CosD(az) - Width;
            y = r * MathHelper.SinD(az) - Height;

            OnPropertyChanged(nameof(XPos), nameof(YPos));
        }

        //private (double, double) GetCartesianCoords()
        //{
        //    var (alt, az) = (CelestialObject.HorizontalCoordinates.Altitude, CelestialObject.HorizontalCoordinates.Azimuth);

        //    double r = (90.0 - alt) / 90.0 * 180.0;

        //    var az_rad = ToRadian(az);

        //    var x = r * Math.Cos(az_rad) - 2.5;
        //    var y = r * Math.Sin(az_rad) - 2.5;

        //    return (x, y);
        //}

        //private double ToRadian(double deg)
        //    => deg * Math.PI / 180.0;

    }
}
