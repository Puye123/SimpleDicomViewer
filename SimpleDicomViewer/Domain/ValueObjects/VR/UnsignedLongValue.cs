using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// UL(Unsigned Long: 符号なし長整数) : uint
    /// </summary>
    /// <remarks>
    /// Todo: 単体テスト
    /// </remarks>
    internal class UnsignedLongValue : ValueElement
    {
        public UnsignedLongValue(Tag tag, byte[] value) : base(tag, value, length: 4, isFixedValue: true, valueType: typeof(uint))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイト固定です。"); }

            try
            {
                uint _ = BitConverter.ToUInt32(value);
            }
            catch (Exception)
            {
                throw new InvalidDICOMFormatException($"符号無し32ビット長2進整数に変換できません");
            }
            return true;
        }
    }
}
