using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace WindowsStoreApp.ViewModels
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return "";
            }

            var formattable = value as IFormattable;
            if (formattable != null)
            {
                return formattable.ToString((string) parameter, CultureInfo.CurrentUICulture);
            }            

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
