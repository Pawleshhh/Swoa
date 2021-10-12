using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Swoa.UI
{
    public class DivideValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double dividend = (double)value;
            double divisor = double.Parse((string)parameter);

            return dividend / divisor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double dividend = (double)value;
            double divisor = double.Parse((string)parameter);

            return dividend * divisor;
        }
    }
}
