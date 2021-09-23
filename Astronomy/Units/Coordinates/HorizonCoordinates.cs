
namespace Astronomy.Units
{
    public struct HorizonCoordinates : IEquatable<HorizonCoordinates>
    {

        #region Constructors

        public HorizonCoordinates(double alt, double az)
        {
            (Altitude, Azimuth) = (alt, az);
        }

        #endregion

        #region Properties

        public double Altitude { get; }
        public double Azimuth { get; }

        #endregion

        #region Methods

        public bool Equals(HorizonCoordinates horizonCoordinates)
            => Altitude.Equals(horizonCoordinates.Altitude) && Azimuth.Equals(horizonCoordinates.Azimuth);

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
