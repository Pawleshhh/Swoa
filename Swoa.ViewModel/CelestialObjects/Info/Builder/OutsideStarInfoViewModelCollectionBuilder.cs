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

            var digitCountFormat = "N2";
            //var timeFormat = "hh\\:mm\\:ss";

            var astrCoordsFormat_ra = "HMS";
            var astrCoordsFormat_degr = "DMS";
            var astrCoordsProvider = new AstronomicCoordsFormatProvider();

            return new ObservableCollection<ICelestialObjectInfoViewModel>()
            {
                GetInfoViewModel("Name", starObj.Name),
                GetInfoViewModel("RA", starObj.EquatorialCoordinates.RightAscension, astrCoordsFormat_ra, astrCoordsProvider),
                GetInfoViewModel("Dec", starObj.EquatorialCoordinates.Declination, astrCoordsFormat_degr, astrCoordsProvider),
                GetInfoViewModel("Alt", () => starObj.HorizonCoordinates.Altitude, astrCoordsFormat_degr, astrCoordsProvider),
                GetInfoViewModel("Az", () => starObj.HorizonCoordinates.Azimuth, astrCoordsFormat_degr, astrCoordsProvider),
                GetInfoViewModel("Visual mag.", starObj.VisualMagnitude, digitCountFormat),
                GetInfoViewModel("Absolute mag.", starObj.AbsoluteMagnitude, digitCountFormat),
                new CelestialObjectMeasureInfoViewModel<double>("Distance To Sun", starObj.DistanceToSun, Units.LightYears) { Format = digitCountFormat },
                //GetInfoViewModel("Rises At", starObj.RisesAt, timeFormat),
                //GetInfoViewModel("Sets At", starObj.SetsAt, timeFormat),
                GetInfoViewModel("Spectral class", starObj.SpectralClass),
                //GetInfoViewModel("Constellation", starObj.Constellation)
            };
        }
    }
}
