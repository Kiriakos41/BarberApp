using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace App1.Models
{
    public class Base64ToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var base64 = (string)value;
            return ImageSource.FromStream(
            () => new MemoryStream(System.Convert.FromBase64String(base64)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
