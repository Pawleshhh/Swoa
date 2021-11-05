using Astronomy.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Astronomy.MathHelper;

namespace Astronomy
{
    public static class AstronomyDateTime
    {

        public static (TimeSpan risesAt, double risesAtAz, TimeSpan setsAt, double setsAtAz) GetRisingAndSettingTime(double ra, double dec, DateTime dateTime, double latitude, double longitude, double v = 0)
        {
            double cosH = -((SinD(v) + SinD(latitude) * SinD(dec)) / (CosD(latitude) * CosD(dec)));

            if (cosH < -1 || cosH > 1)
                return (TimeSpan.Zero, -1, TimeSpan.Zero, -1);

            double h = AcosD(cosH);

            double lst_r = RestoreRange(ra - h, 24);
            double lst_s = RestoreRange(ra + h, 24);

            if (lst_r > lst_s)
                ;

            double a = (SinD(ra) + SinD(v) * SinD(latitude)) / (CosD(v) * CosD(latitude));
            double a_r = RestoreRange(Math.Pow(a, -1), 360);
            double a_s = 360 - a_r;

            //TimeSpan risesAt_gst = TimeSpan.FromHours(ConvertLstToGst(lst_r, longitude));
            //TimeSpan setsAt_gst = TimeSpan.FromHours(ConvertLstToGst(lst_s, longitude));

            //TimeSpan risesAt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, risesAt_gst.Hours, risesAt_gst.Minutes, risesAt_gst.Seconds).ToUniversalTime().TimeOfDay;
            //TimeSpan setsAt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, setsAt_gst.Hours, setsAt_gst.Minutes, setsAt_gst.Seconds).ToUniversalTime().TimeOfDay;

            TimeSpan risesAt;
            TimeSpan setsAt;

            if (lst_r < lst_s)
            {
                risesAt = TimeSpan.FromHours(lst_r);
                setsAt = TimeSpan.FromHours(lst_s);
            }
            else
            {
                risesAt = TimeSpan.FromHours(lst_r);
                setsAt = TimeSpan.FromHours(lst_s).Add(TimeSpan.FromDays(1));
            }


            return (risesAt, a_r, setsAt, a_s);
        }

        public static double ConvertLstToGst(double lst, double longitude)
        {
            double longitudeInHours = longitude / 15.0;

            double r = lst - longitudeInHours;
            return RestoreRange(lst - longitudeInHours, 24);
        }

        private static double RestoreRange(double value, double max)
        {
            while(true)
            {
                if (value < 0)
                    value += max;
                else if (value > max)
                    value -= max;

                if (value >= 0 && value <= max)
                    return value;
            }
        }

    }
}
