using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace RubiusTestAutocadPlugin
{
    public class DoubleToStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double num;
            string strvalue = value as string;
            if (double.TryParse(strvalue, out num))
            {
                return num;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
