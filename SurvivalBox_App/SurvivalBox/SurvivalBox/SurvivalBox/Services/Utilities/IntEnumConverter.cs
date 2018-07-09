using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace SurvivalBox.Services
{
    public class IntEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("-------------------------------------------------------------");
            Debug.WriteLine("-------------------------------------------------------------");
            Debug.WriteLine("-------------------------------------------------------------");
            if (value is Enum)
            {
                return (int)value;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("-------------------------------------------------------------");
            Debug.WriteLine("-------------------------------------------------------------");
            Debug.WriteLine("-------------------------------------------------------------");
            if (value is int)
            {
                return Enum.ToObject(targetType, value);
            }
            return 0;
        }
    }
}