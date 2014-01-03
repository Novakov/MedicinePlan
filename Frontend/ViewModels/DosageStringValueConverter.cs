using System;
using System.Globalization;
using System.Windows.Data;
using MedicinePlan;

namespace Frontend.ViewModels
{
    [ValueConversion(typeof(IDosage), typeof(string))]
    public class DosageStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((dynamic) this).AsString((dynamic) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string AsString(CountPerDayDosage dosage)
        {            
            return string.Format("{0}/doba", dosage.CountPerDay);
        }

    }
}