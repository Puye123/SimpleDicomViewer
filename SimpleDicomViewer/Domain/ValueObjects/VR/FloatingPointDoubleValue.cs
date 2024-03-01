using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// FD(Floating Point Double: 倍精度浮動小数点) : double
    /// </summary>
    /// <remarks>
    /// 倍精度の2進浮動小数点数で，IEEE 754: 1985 の 64 ビット浮動小数点数形式で表現される。 
    /// </remarks>
    public class FloatingPointDoubleValue : ValueElement
    {
        public FloatingPointDoubleValue(Tag tag, byte[] value) : base(tag, value, length: 8, isFixedValue: true, valueType: typeof(double))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は4バイト固定です。"); }

            try
            {
                double _ = BitConverter.ToDouble(value);
            }
            catch (Exception)
            {
                throw new InvalidDICOMFormatException($"倍精度浮動小数点に変換できません");
            }
            return true;
        }
    }
}
