using Microsoft.UI.Xaml.Media.Imaging;
using SimpleDicomViewer.Domain.Entities;
using SimpleDicomViewer.Domain.ValueObjects;
using System.Threading.Tasks;

namespace SimpleDicomViewer.Domain.Services.ImageConverter
{
    public interface IImageConverter
    {
        /// <summary>
        /// DICOMデータからBitmapImage型の画像を生成する
        /// </summary>
        /// <returns>画像</returns>
        public Task<BitmapImage> Convert(byte[] imageArray, int height, int width, int bit, PhotometricInterpretation photometricInterpretation);

        /// <summary>
        /// DICOMデータを画像ファイルとして保存する
        /// </summary>
        /// <returns>画像</returns>
        public string Save(string filepath, byte[] imageArray, int height, int width, int bit, PhotometricInterpretation photometricInterpretation);
    }
}
