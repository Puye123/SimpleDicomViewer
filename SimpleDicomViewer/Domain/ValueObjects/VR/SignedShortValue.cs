using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// SS(Signed Short: 符号付短整数) : short
    /// </summary>
    /// <remarks>
    /// Todo: 単体テスト
    /// </remarks>
    public class SignedShortValue : ValueElement
    {
        public SignedShortValue(Tag tag, byte[] value) : base(tag, value, length: 2, isFixedValue: true, valueType: typeof(short))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイト固定です。"); }

            try
            {
                short _ = BitConverter.ToInt16(value);
            }
            catch (Exception)
            {
                throw new InvalidDICOMFormatException($"2の補数形式の16ビット長符号付き2進整数に変換できません");
            }
            return true;
        }
    }
}
