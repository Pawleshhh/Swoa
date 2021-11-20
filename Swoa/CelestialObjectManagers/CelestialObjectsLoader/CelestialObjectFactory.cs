using Astronomy;
using Astronomy.Units;
using CelestialObjects;
using SwoaDatabaseAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa
{
    public static class CelestialObjectFactory
    {

        public static CelestialObject CreateFromSwoaDbRecord(SwoaDbRecord swoaDbRecord, EquatorialCoordinates equatorialCoordinates, HorizonCoordinates horizonCoordinates, double latitude, double longitude)
        {
            switch (swoaDbRecord)
            {
                case SwoaDbStarRecord starRecord:
                    var risingAndSetting = AstronomyDateTime.GetRisingAndSettingTime(starRecord.Ra, starRecord.Dec, latitude);
                    return new OutsideStarObject()
                    {
                        Id = starRecord.Id,
                        Name = GetStarObjectName(starRecord),
                        EquatorialCoordinates = equatorialCoordinates,
                        HorizonCoordinates = horizonCoordinates,
                        AbsoluteMagnitude = starRecord.AbsMag,
                        VisualMagnitude = starRecord.Mag,
                        DistanceToSun = starRecord.SunDist,
                        DistanceToEarth = starRecord.SunDist,
                        SpectralClass = starRecord.Spect,
                        RisesAt = new DateTime(risingAndSetting.risesAt.Ticks).ToLocalTime().TimeOfDay,
                        SetsAt = new DateTime(risingAndSetting.setsAt.Ticks).ToLocalTime().TimeOfDay
                    };
                default:
                    throw new ArgumentException(nameof(swoaDbRecord));
            }
        }

        private static string GetStarObjectName(SwoaDbStarRecord swoaDbStarRecord)
        {
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Proper))
                return swoaDbStarRecord.Proper;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Bf))
                return swoaDbStarRecord.Bf;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Gl))
                return swoaDbStarRecord.Gl;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Hr))
                return swoaDbStarRecord.Hr;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Hd))
                return swoaDbStarRecord.Hd;
            if (!string.IsNullOrEmpty(swoaDbStarRecord.Hip))
                return swoaDbStarRecord.Hip;

            return "None";
        }

    }
}
