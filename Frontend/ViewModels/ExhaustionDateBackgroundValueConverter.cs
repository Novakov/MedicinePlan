using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Frontend.ViewModels
{
    [ValueConversion(typeof(TimeSpan), typeof(Brush))]
    public class ExhaustionDateBackgroundValueConverter : IValueConverter
    {       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var remainingDays = (TimeSpan)value;

            if (remainingDays.TotalDays <= 2)
            {
                return Brushes.Red;
            }

            if (remainingDays.TotalDays <= 7)
            {
                return Brushes.LightPink;
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}