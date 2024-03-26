using Microsoft.UI.Xaml.Media.Imaging;
using SimpleDicomViewer.Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using SimpleDicomViewer.Domain.Exceptions;
using OpenCvSharp;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using static OpenCvSharp.LineIterator;

namespace SimpleDicomViewer.Domain.Services.ImageConverter
{
    public class RawImageConverter : IImageConverter
    {
        public async Task<BitmapImage> Convert(byte[] imageArray, int height, int width, int bit, PhotometricInterpretation photometricInterpretation)
        {
            // とりあえずRGB8限定 
            if (bit != 8)
            {
                throw new UnsupportImageFormatException("8-bit画像以外は非対応です");
            }
            if (photometricInterpretation.Name != PhotometricInterpretationName.RGB)
            {
                throw new UnsupportImageFormatException("RGB画像以外は非対応です");
            }

            // RGB => RGBA (byte array)
            byte[] rgba = new byte[width * height * 4];
            for (int i = 0; i < width * height; i++)
            {
                rgba[i * 4] = imageArray[i * 3];
                rgba[i * 4 + 1] = imageArray[i * 3 + 1];
                rgba[i * 4 + 2] = imageArray[i * 3 + 2];
                rgba[i * 4 + 3] = 255;
            }

            SoftwareBitmap softwareBitmap = new SoftwareBitmap(BitmapPixelFormat.Rgba8, width, height);
            softwareBitmap.CopyToBuffer(rgba.AsBuffer());

            // SoftwareBitmap -> BitmapImage
            BitmapImage bitmapImage = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, stream);
                encoder.SetSoftwareBitmap(softwareBitmap);
                await encoder.FlushAsync();
                stream.Seek(0);
                bitmapImage.SetSource(stream);
            }

            return bitmapImage;
        }

        public string Save(string filepath, byte[] imageArray, int height, int width, int bit, PhotometricInterpretation photometricInterpretation)
        {
            if (bit == 8)
            {
                if (photometricInterpretation.Name == PhotometricInterpretationName.RGB)
                {
                    var pixels = RRGGBBtoRGBRGB<byte>(imageArray).ToArray();
                    using (Mat image = new Mat(height, width, MatType.CV_8UC3, pixels))
                    {
                        image.SaveImage(filepath);
                    }
                }
                else if (photometricInterpretation.Name == PhotometricInterpretationName.MONOCHROME1)
                {
                    using (Mat image = new Mat(height, width, MatType.CV_8UC1, imageArray))
                    {
                        Cv2.BitwiseNot(image, image);
                        image.SaveImage(filepath);
                    }
                }
                else if (photometricInterpretation.Name == PhotometricInterpretationName.MONOCHROME2)
                {
                    using (Mat image = new Mat(height, width, MatType.CV_8UC1, imageArray))
                    {
                        image.SaveImage(filepath);
                    }
                }
            }
            else if (bit == 16)
            {
                if (photometricInterpretation.Name == PhotometricInterpretationName.RGB)
                {
                    var pixels = Byte16toUshort16(imageArray);
                    pixels = RRGGBBtoRGBRGB<ushort>(pixels).ToArray();
                    using (Mat image = new Mat(height, width, MatType.CV_16UC3, pixels))
                    {
                        image.SaveImage(filepath);
                    }
                }
                else if (photometricInterpretation.Name == PhotometricInterpretationName.MONOCHROME1)
                {
                    var pixels = Byte16toUshort16(imageArray);
                    using (Mat image = new Mat(height, width, MatType.CV_16UC1, pixels))
                    {
                        image.SaveImage(filepath);
                    }
                }
                else if (photometricInterpretation.Name == PhotometricInterpretationName.MONOCHROME2)
                {
                    var pixels = Byte16toUshort16(imageArray);
                    using (Mat image = new Mat(height, width, MatType.CV_16UC1, pixels))
                    {
                        image.SaveImage(filepath);
                    }
                }
            }
            else if (bit == 12)
            {
                if (photometricInterpretation.Name == PhotometricInterpretationName.RGB)
                {
                    var pixels = Byte12toUshort16(imageArray);
                    pixels = RRGGBBtoRGBRGB<ushort>(pixels).ToArray();
                    using (Mat image = new Mat(height, width, MatType.CV_16UC3, pixels))
                    {
                        image.SaveImage(filepath);
                    }
                }
                else if (photometricInterpretation.Name == PhotometricInterpretationName.MONOCHROME1)
                {
                    var pixels = Byte12toUshort16(imageArray);
                    using (Mat image = new Mat(height, width, MatType.CV_16UC1, pixels))
                    {
                        image.SaveImage(filepath);
                    }
                }
                else if (photometricInterpretation.Name == PhotometricInterpretationName.MONOCHROME2)
                {
                    var pixels = Byte12toUshort16(imageArray);
                    using (Mat image = new Mat(height, width, MatType.CV_16UC1, pixels))
                    {
                        image.SaveImage(filepath);
                    }
                }
            }
            else
            {
                throw new UnsupportImageFormatException($"非対応フォーマットです bit = {bit}");
            }
            
            return filepath;
        }

        private IList<T> RRGGBBtoRGBRGB<T>(IList<T> list)
        {
            var pixels = new T[list.Count];
            int pixelPerPlane = (int)pixels.Length / 3;
            for (int i = 0; i < pixelPerPlane; i++)
            {
                pixels[i * 3 + 0] = list[i + pixelPerPlane * 0];
                pixels[i * 3 + 1] = list[i + pixelPerPlane * 1];
                pixels[i * 3 + 2] = list[i + pixelPerPlane * 2];
            }
            return pixels;
        }

        private ushort[] Byte12toUshort16(byte[] input)
        {
            ushort[] output = new ushort[input.Length / 2];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (ushort)((ushort)((input[i * 2 + 1] << 8) | input[i * 2]) << 4);
            }

            return output;
        }

        private ushort[] Byte16toUshort16(byte[] input)
        {
            ushort[] output = new ushort[input.Length / 2];

            for (int i = 0; i < output.Length; i ++)
            {
                output[i] = (ushort)(ushort)(input[i*2 + 1] << 8 | input[i*2]);
            }

            return output;
        }
    }
}
