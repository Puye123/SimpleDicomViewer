using SimpleDicomViewer.Domain.ValueObjects;

namespace SimpleDicomViewer.Domain.Services.ImageConverter
{
    public interface IImageConverterFactory
    {
        IImageConverter CreateImageConverter(TransferSyntaxName transferSyntaxName);
    }
}
