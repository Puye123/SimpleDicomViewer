using CommunityToolkit.Mvvm.ComponentModel;
using SimpleDicomViewer.Domain.StaticValues;
using SimpleDicomViewer.Domain.ValueObjects.VR;
using System.Diagnostics;
using System;
using System.Linq;

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
            var obj = valueElement.GetValueObject();
            if (valueElement.ValueType.IsArray && valueElement.ValueType != typeof(byte[])) {
                //object[] objAsArray = (object[])obj;
                if (obj is Array objAsArray)
                {
                    string str = "[";

                    for (int i = 0; i < objAsArray.Length; ++i)
                    {
                        str += objAsArray.GetValue(i).ToString();
                        if (i != objAsArray.Length - 1)
                        {
                            str += ", ";
                        }
                    }
                    str += "]";
                    data = str;
                }                
            }
            else if (valueElement.ValueType == typeof(byte[]))
            {
                var byteArray = (byte[])valueElement.GetValueObject();
                if (byteArray.Length < 30)
                {
                    data = BitConverter.ToString(byteArray);
                }
                else
                {
                    data = BitConverter.ToString(byteArray.Take(30).ToArray()) + " ...";
                }
            }
            else
            {
                data = valueElement.GetValueObject().ToString();
            }
        }
    }
}
