using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Tranee.Converters
{
    
        public class IntToStringConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                
                if (value == null) return string.Empty;

               
                if (value is int intVal && intVal == 0) return string.Empty;

                return value.ToString();
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string strValue = value as string;

                if (string.IsNullOrWhiteSpace(strValue))
                    return 0; 

                if (int.TryParse(strValue, out int result))
                    return result; 

                return 0;
            }
        }
    
}
