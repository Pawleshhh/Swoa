using CelestialObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class OutsideStarInfoViewModelCollectionBuilder : CelestialObjectInfoViewModelCollectionBuilder
    {
        public override ObservableCollection<ICelestialObjectInfoViewModel> BuildCelestialObjectInfoViewModelCollection(CelestialObjectViewModel celestialObjectVM)
        {
            OutsideStarObject starObj = (OutsideStarObject)celestialObjectVM.CelestialObject;

            var format = "N2";

            return new ObservableCollection<ICelestialObjectInfoViewModel>()
            {
                GetInfoViewModel("Name", starObj.Name),
                GetInfoViewModel("RA", starObj.EquatorialCoordinates.RightAscension, format),
                GetInfoViewModel("Dec", starObj.EquatorialCoordinates.Declination, format),
                GetInfoViewModel("Alt", starObj.HorizonCoordinates.Altitude, format),
                GetInfoViewModel("Az", starObj.HorizonCoordinates.Azimuth, format),
                GetInfoViewModel("Visual mag.", starObj.VisualMagnitude, format),
                GetInfoViewModel("Absolute mag.", starObj.AbsoluteMagnitude, format),
                new CelestialObjectMeasureInfoViewModel<double>("Distance To Sun", starObj.DistanceToSun, Units.LightYears) { Format = format },
                GetInfoViewModel("Rises At", starObj.RisesAt),
                GetInfoViewModel("Sets At", starObj.SetsAt),
                GetInfoViewModel("Spectral class", starObj.SpectralClass),
                GetInfoViewModel("Constellation", starObj.Constellation)
            };
        }
    }
}
