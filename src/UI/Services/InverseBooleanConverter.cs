using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NugetCompare.UI.Services
{
    /// <summary>
    /// As seen on StackOverflow: http://stackoverflow.com/questions/1039636/how-to-bind-inverse-boolean-properties-in-wpf
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }

            if (parameter == null)
            {
                return flag ? Visibility.Visible : Visibility.Collapsed;
            }

            if (bool.Parse((string)parameter))
            {
                flag = !flag;
            }
            return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var back = ((value is Visibility) && (((Visibility)value) == Visibility.Visible));
            if (parameter == null)
            {
                return back;
            }

            if ((bool)parameter)
            {
                back = !back;
            }
            return back;
        }
    }
}
