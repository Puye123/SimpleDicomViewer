using CommunityToolkit.Mvvm.ComponentModel;
using SimpleDicomViewer.Domain.StaticValues;
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
            var dict = TagDictionary.GetInstance();
            var tagInfo = dict.Search(valueElement.Tag);
            name = tagInfo.Description;
            length = valueElement.Value?.Length;
            data = valueElement.GetValueObject().ToString();
        }
    }
}
