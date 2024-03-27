using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// LT(Long Text: 長テキスト) : string
    /// </summary>
    /// <remarks>
    /// Todo: 単体テスト作成
    /// </remarks>
    public class LongTextValue : ValueElement
    {
        public LongTextValue(Tag tag, byte[] value) : base("LT", tag, value, length: 10240, isFixedValue: false, valueType: typeof(string))
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
