using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Swoa.UI
{
    public class MultipleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double multiplicand = (double)value;
            double multiplier = double.Parse((string)parameter);

            return multiplicand * multiplier;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double multiplicand = (double)value;
            double multiplier = double.Parse((string)parameter);

            return multiplicand / multiplier;
        }
    }
}
