using System;
using System.Globalization;
using System.Windows.Data;
using MedicinePlan;

namespace Frontend.ViewModels
{
    [ValueConversion(typeof(IDosage), typeof(string))]
    public class DosageStringValueConverter : IValueConverter
    {
        private static readonly DosageHelper Helper = new DosageHelper();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Helper.GetDescription((IDosage)value);
        }       

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }        
    }

    public class DosageHelper
    {
        public string GetDescription(IDosage value)
        {
            return ((dynamic)this).AsString((dynamic)value);
        }

        private string AsString(NoDosage dosage)
        {
            return "Nie przyjmowane";
        }

        private string AsString(CountPerDayDosage dosage)
        {
            return string.Format("{0}/doba", dosage.CountPerDay);
        }
    }
}