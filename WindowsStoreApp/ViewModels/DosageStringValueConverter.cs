using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
using MedicinePlan;

namespace WindowsStoreApp.ViewModels
{    
    public class DosageStringValueConverter : IValueConverter
    {
        private static readonly DosageHelper Helper = new DosageHelper();       

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Helper.GetDescription((IDosage)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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

        private string AsString(CountPerDayDosage dosage)
        {
            return string.Format("{0} per day", dosage.CountPerDay);
        }
    }
}