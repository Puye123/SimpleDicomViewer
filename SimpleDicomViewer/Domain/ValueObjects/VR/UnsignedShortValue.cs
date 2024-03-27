using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// US(Unsigned Short: 符号なし短整数) : ushort
    /// </summary>
    /// <remarks>
    /// Todo: 単体テスト
    /// </remarks>
    public class UnsignedShortValue : ValueElement
    {
        public UnsignedShortValue(Tag tag, byte[] value) : base("US", tag, value, length: 2, isFixedValue: true, valueType: typeof(ushort))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイト固定です。"); }

            try
            {
               ushort _ = BitConverter.ToUInt16(value);
            }
            catch (Exception)
            {
                throw new InvalidDICOMFormatException($"符号無し16ビット長2進整数に変換できません");
            }
            return true;
        }
    }
}
