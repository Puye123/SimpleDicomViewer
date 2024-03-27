namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// UT(UNlimited Text: 無制限テキスト) : string
    /// </summary>
    /// <remarks>
    /// Todo: 仕様未実装
    /// </remarks>
    public class UNlimitedTextValue : ValueElement
    {
        public UNlimitedTextValue(Tag tag, byte[] value) : base("UT", tag, value, length: uint.MaxValue - 2, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            return true;
        }
    }
}
