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
        }

        public CelestialObject CelestialObject { get; }

        private double x;
        private double y;

        private double height = 12;
        private double width = 12;

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
