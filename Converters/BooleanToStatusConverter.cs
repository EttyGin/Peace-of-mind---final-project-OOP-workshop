using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace loginDb.Converters
{
    public class BooleanToStatusConverter : IValueConverter
    {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Open" : "Close";
            }
            return "Close";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.Equals("Open", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }

}
