using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// ST(Short Text: 短テキスト) : string
    /// </summary>
    /// <remarks>
    /// Todo: 単体テスト
    /// </remarks>
    public class ShortTextValue : ValueElement
    {
        public ShortTextValue(Tag tag, byte[] value) : base(tag, value, length: 1024, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            string valueString = System.Text.Encoding.ASCII.GetString(value);
            if (valueString.Length > Length)
            {
                throw new InvalidDICOMFormatException($"設定可能な文字列は最大{Length}文字です。");
            }

            return true;
        }
    }
}
