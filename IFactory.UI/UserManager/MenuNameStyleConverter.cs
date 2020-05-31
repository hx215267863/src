using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IFactory.UI.UserManager
{
    [ValueConversion(typeof(int), typeof(Style))]
    public class MenuNameStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 1:
                    return Application.Current.FindResource("TopMenuNameStyle");
                case 2:
                    return Application.Current.FindResource("SecondMenuNameStyle");
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (targetType == typeof(int));
        }
    }
}
