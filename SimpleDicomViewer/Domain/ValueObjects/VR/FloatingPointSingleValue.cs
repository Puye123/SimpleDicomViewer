using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// FL(Floating Point Single: 単精度浮動小数点) : float
    /// </summary>
    /// <remarks>
    /// 単精度の2進浮動小数点数で，IEEE 754: 1985 の 32ビット浮動小数点数形式で表現される。
    /// </remarks>
    public class FloatingPointSingleValue : ValueElement
    {
        public FloatingPointSingleValue(Tag tag, byte[] value) : base("FL", tag, value, length: 4, isFixedValue: true, valueType: typeof(float))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は4バイト固定です。"); }

            try
            {
                float _ = BitConverter.ToSingle(value);
            }
            catch (Exception)
            {
                throw new InvalidDICOMFormatException($"単精度浮動小数点に変換できません");
            }
            return true;
        }
    }
}
