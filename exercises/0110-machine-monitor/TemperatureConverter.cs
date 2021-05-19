using System;
using System.Globalization;
using System.Windows.Data;

namespace MachineMonitor
{
    /// <summary>
    /// Converts a temperature to the height of a bar chart
    /// </summary>
    public class TemperatureConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo __)
        {
            if (
                // First parameter has to be temperature reading
                values[0] is double temperature
                // Second parameter has to be max height of bar
                && values[1] is double maxHeight
                // Target has to be double (we cannot convert into anything else)
                && targetType == typeof(double)
                // Parameter has to be max temperature
                && parameter is double maxTemperature)
            {
                // Note: We only use 98% of the max height just to be sure that bar
                //       fits into visible area. Dirty trick, but works.
                return temperature / maxTemperature * maxHeight * 0.98d;
            }

            throw new InvalidCastException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
