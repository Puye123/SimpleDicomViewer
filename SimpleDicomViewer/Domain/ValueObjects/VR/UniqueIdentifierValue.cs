using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// UI(Unique Identifier: 固有識別子) : string
    /// </summary>
    /// <remarks>
    /// Todo: UIDは独自値オブジェクトとして持つべきか
    /// </remarks>
    public class UniqueIdentifierValue : ValueElement
    {
        public UniqueIdentifierValue(Tag tag, byte[] value) : base("UI", tag, value, length: 64, isFixedValue: true, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length > Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイト以下です。"); }

            return true;
        }
    }
}
