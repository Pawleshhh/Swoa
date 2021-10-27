using CelestialObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa
{
    public class CelestialObjectReviewer
    {

        #region Constructors

        public CelestialObjectReviewer()
        {

        }

        #endregion

        #region Fields

        private double maxMagnitude = 7.0;
        private double minMagnitude = double.MinValue;

        #endregion

        #region Properties

        public double MaxMagnitude
        {
            get => maxMagnitude;
            set
            {
                maxMagnitude = value;
            }
        }

        public double MinMagnitude
        {
            get => minMagnitude;
            set
            {
                minMagnitude = value;
            }
        }

        #endregion

        #region Methods

        public virtual bool Review(CelestialObject celestialObject)
        {
            var mag = celestialObject.VisualMagnitude;

            return minMagnitude <= mag && mag <= maxMagnitude;
        }

        #endregion

    }
}
