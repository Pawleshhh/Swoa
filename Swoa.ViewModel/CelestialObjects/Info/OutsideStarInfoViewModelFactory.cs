using CelestialObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class OutsideStarInfoViewModelFactory : CelestialObjectInfoViewModelFactory
    {
        public override ObservableCollection<ICelestialObjectInfoViewModel> CreateCelestialObjectInfoViewModels(CelestialObjectViewModel celestialObjectVM)
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
                GetInfoViewModel("Distance To Sun", starObj.DistanceToSun),
                GetInfoViewModel("Distance To Earth", starObj.DistanceToEarth),
                GetInfoViewModel("Rises At", starObj.RisesAt),
                GetInfoViewModel("Sets At", starObj.SetsAt),
            };
        }
    }
}
