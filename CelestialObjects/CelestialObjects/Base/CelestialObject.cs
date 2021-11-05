using Astronomy.Units;
using System;
using Utilities;
using static Utilities.PropertyChangedHelper;

namespace CelestialObjects
{
    public abstract record CelestialObject
    {
        public long Id { get; init; }

        public string Name { get; init; } = string.Empty;
        public EquatorialCoordinates EquatorialCoordinates { get; init; }

        private HorizonCoordinates horizonCoordinates;
        public HorizonCoordinates HorizontalCoordinates
        {
            get => horizonCoordinates;
            set => SetProperty(ref horizonCoordinates, value, OnHorizonCoordsChanged);
        }

        public double VisualMagnitude { get; init; }
        public double DistanceToSun { get; init; }
        public double DistanceToEarth { get; init; }
        public TimeSpan RisesAt { get; init; }
        public TimeSpan SetsAt { get; init; }

        public event EventHandler<DataChangedEventArgs<HorizonCoordinates>>? HorizonCoordsChanged;

        protected void OnHorizonCoordsChanged(HorizonCoordinates prev, HorizonCoordinates curr)
            => HorizonCoordsChanged?.Invoke(this, new DataChangedEventArgs<HorizonCoordinates>(prev, curr));

    }
}