using Astronomy;
using Astronomy.Units;
using CelestialObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Swoa.ViewModel
{
    public class CelestialObjectViewModel : NotifyPropertyChanges
    {

        public CelestialObjectViewModel(CelestialObject celestialObject)
        {
            CelestialObject = celestialObject;
            celestialObject.HorizonCoordsChanged += CelestialObject_HorizonCoordsChanged;

            double size = 0;
            if (celestialObject.VisualMagnitude > 4)
                size = 1.0;
            else if (celestialObject.VisualMagnitude > 2)
                size = 1.5;
            else if (celestialObject.VisualMagnitude > 0)
                size = 2.5;
            else
                size = 3.5;

            height = width = size;

            SetColor();
        }

        private double x;
        private double y;

        private double height = 2;
        private double width = 2;

        private string color;

        public CelestialObject CelestialObject { get; }

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

        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }

        public event EventHandler<DataChangedEventArgs<HorizonCoordinates>>? HorizonCoordsChanged;

        protected void OnHorizonCoordsChanged(DataChangedEventArgs<HorizonCoordinates> e)
            => HorizonCoordsChanged?.Invoke(this, e);

        private void CelestialObject_HorizonCoordsChanged(object sender, DataChangedEventArgs<HorizonCoordinates> e)
        {
            OnHorizonCoordsChanged(e);
        }

        private void SetColor()
        {
            try
            {
                var spectralClass = ((StarObject)CelestialObject).SpectralClass;

                if (spectralClass != null && spectralClass.Length > 0)
                {
                    var c = spectralClass[0];

                    if (c == 'O')
                        color = "DeepSkyBlue";
                    else if (c == 'B')
                        color = "LightSkyBlue";
                    else if (c == 'A')
                        color = "LightBlue";
                    else if (c == 'F')
                        color = "White";
                    else if (c == 'G')
                        color = "PapayaWhip";
                    else if (c == 'K')
                        color = "Gold";
                    else if (c == 'M')
                        color = "Orange";
                    else
                        color = "Red";
                }
            }
            catch
            {
                color = "Red";
            }
        }

    }
}
