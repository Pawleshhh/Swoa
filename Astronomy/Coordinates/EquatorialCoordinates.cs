
using System;

namespace Astronomy.Units
{
    public struct EquatorialCoordinates : IEquatable<EquatorialCoordinates>, IComparable<EquatorialCoordinates>
    {

        #region Constructors

        public EquatorialCoordinates(double dec, double ra)
        {
            (Declination, RightAscension) = (dec, ra);
        }

        #endregion

        #region Properties

        public double Declination { get; }
        public double RightAscension { get; }

        #endregion

        #region Methods

        public bool Equals(EquatorialCoordinates equatorialCoordinates)
            => Declination.Equals(equatorialCoordinates.Declination) && RightAscension.Equals(equatorialCoordinates.RightAscension);

        public int CompareTo(EquatorialCoordinates other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is EquatorialCoordinates coordinates)
                return Equals(coordinates);

            return false;
        }

        public override int GetHashCode()
        {
            return (Declination.GetHashCode()) * 17 + (RightAscension.GetHashCode() * 13);
        }

        public override string ToString()
        {
            return $"{Declination} DEC, {RightAscension} RA";
        }

        #endregion

    }
}
