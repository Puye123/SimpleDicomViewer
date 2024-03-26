using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using Windows.Storage.Streams;

namespace SimpleDicomViewer.ViewModels
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is byte[] byteArray)
            {
                using (var stream = new InMemoryRandomAccessStream())
                {
                    stream.AsStreamForWrite().Write(byteArray, 0, byteArray.Length);
                    var image = new BitmapImage();
                    image.SetSource(stream);
                    return image;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
