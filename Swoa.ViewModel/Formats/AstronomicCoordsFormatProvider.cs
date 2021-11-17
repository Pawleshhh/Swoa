using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class AstronomicCoordsFormatProvider : IFormatProvider, ICustomFormatter
    {

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            // Check whether this is an appropriate callback
            if (!this.Equals(formatProvider))
                return null;

            // Set default format specifier
            if (string.IsNullOrEmpty(format))
                format = "DMS";

            double value = (double)arg;

            var results = new string[format.Length];
            int i = 0;

            foreach (var c in format.ToLower())
            {
                if (c == 'd')
                    results[i] = Format_D(value);
                else if (c == 'm')
                    results[i] = Format_M(value);
                else if (c == 's')
                    results[i] = Format_S(value);
                else if (c == 'h')
                    results[i] = Format_H(value);
                else
                    throw new FormatException($"Unrecognizable format - {c}");

                i++;
            }

            return string.Join(' ', results);
        }

        private string Format_D(double value)
        {
            return ((int)value).ToString() + "°";
        }

        private string Format_H(double value)
        {
            return ((int)GetTimePart(value, 24.0)) + "h";
        }

        private string Format_M(double value)
        {
            return ((int)GetTimePart(value)) + "'";
        }

        private string Format_S(double value)
        {
            return ((int)GetTimePart(GetTimePart(value))) + "''";
        }

        private double GetTimePart(double value, double part = 60.0)
        {
            int intPart = (int)value;
            double fractPart = value - intPart;

            return (fractPart * part);
        }

    }
}
