using Astronomy;
using Astronomy.Units;
using CelestialObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utilities;

namespace Swoa.ViewModel
{
    public class CelestialObjectViewModel : NotifyPropertyChanges
    {

        #region Constructors

        public CelestialObjectViewModel(CelestialObject celestialObject)
        {
            CelestialObject = celestialObject;
            celestialObject.HorizonCoordsChanged += CelestialObject_HorizonCoordsChanged;

            SetSize();
            SetColor();
        }

        #endregion

        #region Fields

        private double x;
        private double y;

        private double height = 2;
        private double width = 2;

        private bool isSelected;

        private string color;

        private ReadOnlyObservableCollection<ICelestialObjectInfoViewModel> celestialObjectInfoCollection;

        #endregion

        #region Properties

        public CelestialObject CelestialObject { get; }

        public bool IsVisible => CelestialObject.HorizonCoordinates.Altitude >= 0;

        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

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

        public ReadOnlyObservableCollection<ICelestialObjectInfoViewModel> CelestialObjectInfoCollection =>
            celestialObjectInfoCollection ??= new ReadOnlyObservableCollection<ICelestialObjectInfoViewModel>(
                GetCelestialObjectInfoViewModelFactory().BuildCelestialObjectInfoViewModelCollection(this));

        #endregion

        #region Events

        public event EventHandler<DataChangedEventArgs<HorizonCoordinates>> HorizonCoordsChanged;
        public event EventHandler Selected;

        protected void OnHorizonCoordsChanged(DataChangedEventArgs<HorizonCoordinates> e)
            => HorizonCoordsChanged?.Invoke(this, e);
        protected void OnSelected()
            => Selected?.Invoke(this, EventArgs.Empty);

        #endregion

        #region Methods

        private void CelestialObject_HorizonCoordsChanged(object sender, DataChangedEventArgs<HorizonCoordinates> e)
        {
            OnPropertyChanged(nameof(CelestialObject), nameof(IsVisible));
            OnHorizonCoordsChanged(e);
        }

        protected virtual void SetSize()
        {
            double size;
            if (CelestialObject.VisualMagnitude > 4)
                size = 1.0;
            else if (CelestialObject.VisualMagnitude > 2)
                size = 1.5;
            else if (CelestialObject.VisualMagnitude > 0)
                size = 2.5;
            else
                size = 3.5;

            height = width = size;
        }

        protected virtual void SetColor()
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

        protected virtual CelestialObjectInfoViewModelCollectionBuilder GetCelestialObjectInfoViewModelFactory()
        {
            switch (CelestialObject)
            {
                case OutsideStarObject s:
                    return new OutsideStarInfoViewModelCollectionFactory();
                default:
                    throw new InvalidOperationException("Unrecognized type of CelestialObject");
            }
        }

        #endregion

        #region Commands

        private ICommand select;
        public ICommand Select => RelayCommand.Create(
            ref select,
            _ =>
            {
                IsSelected = !IsSelected;
                OnSelected();
            });

        #endregion

    }
}
