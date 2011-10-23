using System;
using System.Globalization;
using System.Windows.Data;

namespace Core.Converters
{
    public class StringToDoubleConveter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double convertingValue;
            var success = Double.TryParse(value as string, out convertingValue);
            if (!success)
                throw new ArgumentException(String.Format("Не удалось преобразовать значение {0}", value));

            return convertingValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
