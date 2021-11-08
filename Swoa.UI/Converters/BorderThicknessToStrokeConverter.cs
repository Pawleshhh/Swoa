using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Swoa.UI
{
    public class BorderThicknessToStrokeThickness : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(double)) throw new ArgumentException("Expected double type target");

            var thickness = (Thickness)value;
            double max = Math.Max(thickness.Top, Math.Max(thickness.Bottom, Math.Max(thickness.Left, thickness.Right)));

            return max;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Thickness)) throw new ArgumentException("Expected thickness type target");

            double strokeThickness = (double)value;

            return new Thickness(strokeThickness);
        }
    }
}
