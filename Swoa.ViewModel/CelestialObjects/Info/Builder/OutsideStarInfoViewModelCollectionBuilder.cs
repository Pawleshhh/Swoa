using CelestialObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            return new ObservableCollection<ICelestialObjectInfoViewModel>()
            {
                GetInfoViewModel("Name", starObj.Name),
                GetInfoViewModel("RA", starObj.EquatorialCoordinates.RightAscension),
                GetInfoViewModel("Dec", starObj.EquatorialCoordinates.Declination),
                GetInfoViewModel("Alt", starObj.HorizonCoordinates.Altitude),
                GetInfoViewModel("Az", starObj.HorizonCoordinates.Azimuth),
                GetInfoViewModel("Visual mag.", starObj.VisualMagnitude),
                GetInfoViewModel("Absolute mag.", starObj.AbsoluteMagnitude),
                new CelestialObjectMeasureInfoViewModel<double>("Distance To Sun", starObj.DistanceToSun, Units.LightYears),
                GetInfoViewModel("Rises At", starObj.RisesAt),
                GetInfoViewModel("Sets At", starObj.SetsAt),
                GetInfoViewModel("Spectral class", starObj.SpectralClass),
                GetInfoViewModel("Constellation", starObj.Constellation)
            };
        }
    }
}
