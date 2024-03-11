using CommunityToolkit.Mvvm.ComponentModel;
using SimpleDicomViewer.Domain.ValueObjects.VR;

namespace SimpleDicomViewer.ViewModels
{
    public partial class TagListElement : ObservableObject
    {
        [ObservableProperty]
        string? tag;

        [ObservableProperty]
        string? name;

        [ObservableProperty]
        int? length;

        [ObservableProperty]
        string? data;

        public ValueElement ValueElement { get; }

        public TagListElement(ValueElement valueElement)
        {
            this.ValueElement = valueElement;
            tag = valueElement.Tag.ToString();
            name = "Not Implemented";
            length = valueElement.Value?.Length;
            data = valueElement.GetValueObject().ToString();
        }
    }
}
