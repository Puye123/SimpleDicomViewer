using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    public class AttributeTagValue : ValueElement
    {
        public AttributeTagValue(Tag tag, byte[] value) : base(tag, value, length: 4, isFixedValue: true, valueType: typeof(Tag))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイトのみです。"); }

            return true;
        }
    }
}
