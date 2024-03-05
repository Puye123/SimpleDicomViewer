namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// UN(Unknown: 未知) : string
    /// </summary>
    /// <remarks>
    /// 仕様未実装
    /// </remarks>
    public class UnknownValue : ValueElement
    {
        public UnknownValue(Tag tag, byte[] value) : base(tag, value, length: uint.MaxValue, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            return true;
        }
    }
}
