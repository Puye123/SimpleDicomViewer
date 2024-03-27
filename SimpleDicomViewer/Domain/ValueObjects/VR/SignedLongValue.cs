using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// SL(Signed Long: 符号付長整数): int
    /// </summary>
    /// <remarks>
    /// Todo: 単体テスト
    /// </remarks>
    public class SignedLongValue : ValueElement
    {
        public SignedLongValue(Tag tag, byte[] value) : base("SL", tag, value, length: 4, isFixedValue: true, valueType: typeof(int))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイト固定です。"); }

            try
            {
                int _ = BitConverter.ToInt32(value);
            }
            catch (Exception)
            {
                throw new InvalidDICOMFormatException($"2の補数形式の32ビット長符号付き2進整数に変換できません");
            }
            return true;
        }
    }
}
