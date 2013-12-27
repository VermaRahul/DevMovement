using System;

namespace Movie_Search.Converter
{
    public class ImageResolutionConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string link = App.Current.ImagesBaseLink;

            link = link + (parameter as string);

            link = link + (value as string);

            return link;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
