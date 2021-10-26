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

        public CelestialObjectViewModel(CelestialObject celestialObject)
        {
            CelestialObject = celestialObject;

            double size = 0;
            if (celestialObject.VisualMagnitude > 4)
                size = 0.5;
            else if (celestialObject.VisualMagnitude > 2)
                size = 1.0;
            else if (celestialObject.VisualMagnitude > 0)
                size = 2.0;
            else
                size = 3.0;

            height = width = size;
        }

        public CelestialObject CelestialObject { get; }

        private double x;
        private double y;

        private double height = 2;
        private double width = 2;

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
            set => SetProperty(() => height == value, () => height = value);
        }

        public double Width
        {
            get => width;
            set => SetProperty(() => width == value, () => width = value);
        }

    }
}
