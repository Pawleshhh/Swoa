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

        public static CelestialObject CreateFromSwoaDbRecord(SwoaDbRecord swoaDbRecord, EquatorialCoordinates equatorialCoordinates, HorizonCoordinates horizonCoordinates)
        {
            switch (swoaDbRecord)
            {
                case SwoaDbStarRecord starRecord:
                    return new OutsideStarObject()
                    {
                        Id = starRecord.Id,
                        Name = GetStarObjectName(starRecord),
                        EquatorialCoordinates = equatorialCoordinates,
                        HorizonCoordinates = horizonCoordinates,
                        VisualMagnitude = starRecord.Mag,
                        DistanceToSun = starRecord.SunDist,
                        DistanceToEarth = starRecord.SunDist,
                        SpectralClass = (starRecord).Spect
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
