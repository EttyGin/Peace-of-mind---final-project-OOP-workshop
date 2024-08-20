using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace loginDb.Converters
{
    public class TextToFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int text)
            {
                int len = text.ToString().Length;
                double fontSize = 60 - 5*len; 
                if (fontSize < 30) fontSize += 5;
                return fontSize;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
