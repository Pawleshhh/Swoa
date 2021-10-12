using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy
{
    public static class MathHelper
    {

        public static double SinD(double a)
            => Math.Sin(DegreesToRadians(a));

        public static double CosD(double a)
            => Math.Cos(DegreesToRadians(a));

        public static double TanD(double a)
            => Math.Tan(DegreesToRadians(a));

        public static double DegreesToRadians(double degrees)
            => degrees * Math.PI / 180.0;

        public static double RadiansToDegrees(double radians)
            => radians * 180.0 / Math.PI;

    }
}
