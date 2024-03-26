using SimpleDicomViewer.Domain.ValueObjects;
using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.Services.ImageConverter
{
    public class ImageConverterFactory : IImageConverterFactory
    {
        public IImageConverter CreateImageConverter(TransferSyntaxName transferSyntaxName)
        {
            return transferSyntaxName switch
            {
                TransferSyntaxName.Undifine => throw new UnsupportImageFormatException(),

                TransferSyntaxName.ImplicitVREndian => new RawImageConverter(),
                TransferSyntaxName.ExplicitVRLittleEndian => new RawImageConverter(),
                TransferSyntaxName.DeflatedExplicitVRLittleEndian => new RawImageConverter(),
                TransferSyntaxName.ExplicitVRBigEndian => new RawImageConverter(),

                TransferSyntaxName.JPEGBaselineProcess2and4 => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPEGLosslessProcess14 => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPEGLosslessProcess14SelectionValue1 => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPEG_LSLossless => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPEG_LSLossy => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPEG2000LosslessOnly => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPEG2000 => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPEG2000Part2LosslessOnly => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPEG2000Part2 => throw new UnsupportImageFormatException(),

                TransferSyntaxName.JPIPReferenced => throw new UnsupportImageFormatException(),
                TransferSyntaxName.JPIPReferencedDeflate => throw new UnsupportImageFormatException(),

                TransferSyntaxName.RLELossless => throw new UnsupportImageFormatException(),

                TransferSyntaxName.RFC2557MIMEEncapsulation => throw new UnsupportImageFormatException(),

                TransferSyntaxName.MPEG2 => throw new UnsupportImageFormatException(),
                TransferSyntaxName.MPEG4HighProfile => throw new UnsupportImageFormatException(),
                TransferSyntaxName.MPEG4BDcompatibleHighProfile => throw new UnsupportImageFormatException(),

                _ => throw new UnsupportImageFormatException()
            };
        }
    }
}
