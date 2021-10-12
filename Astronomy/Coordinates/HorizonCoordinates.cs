
using System;

namespace Astronomy.Units
{
    public struct HorizonCoordinates : IEquatable<HorizonCoordinates>, IComparable<HorizonCoordinates>
    {

        #region Constructors

        public HorizonCoordinates(double alt, double az)
        {
            (Altitude, Azimuth) = (alt, az);
        }

        #endregion

        #region Const

        public const double MAXALTITUDE = 90.0;
        public const double MINALTITUDE = 0.0;

        public const double MAXAZIMUTH = 360.0;
        public const double MINAZIMUTH = 0.0;

        #endregion

        #region Properties

        public double Altitude { get; }
        public double Azimuth { get; }

        #endregion

        #region Methods

        public int CompareTo(HorizonCoordinates other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(HorizonCoordinates horizonCoordinates)
            => Altitude.Equals(horizonCoordinates.Altitude) && Azimuth.Equals(horizonCoordinates.Azimuth);

        public void Deconstruct(out double alt, out double az)
        {
            alt = Altitude;
            az = Azimuth;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is HorizonCoordinates coordinates)
                return Equals(coordinates);

            return false;
        }

        public override int GetHashCode()
        {
            return (Altitude.GetHashCode()) * 17 + (Azimuth.GetHashCode() * 13);
        }

        public override string ToString()
        {
            return $"{Altitude} Alt, {Azimuth} Az";
        }

        #endregion

    }
}
