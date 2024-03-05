using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// LO(Long String: 長列) : string
    /// </summary>
    /// <remarks>
    /// Todo 単体テスト作成
    /// </remarks>
    public class LongStringValue : ValueElement
    {
        public LongStringValue(Tag tag, byte[] value) : base(tag, value, length: 64, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            string valueString = System.Text.Encoding.ASCII.GetString(value);
            if(valueString.Length > Length ) {
                throw new InvalidDICOMFormatException($"設定可能な文字列は最大{Length}文字です。");
            }

            return true;
        }
    }
}
