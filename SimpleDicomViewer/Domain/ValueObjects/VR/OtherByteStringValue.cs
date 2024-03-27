namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// OB(Other Byte String: その他のバイト列) : byte[]
    /// </summary>
    /// <remarks>
    /// 値チェックなどの仕様は未実装
    /// </remarks>
    public class OtherByteStringValue : ValueElement
    {
        public OtherByteStringValue(Tag tag, byte[] value) : base("OB", tag, value, length : 1024, isFixedValue: false, valueType: typeof(byte[]))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            return true;
        }
    }
}
