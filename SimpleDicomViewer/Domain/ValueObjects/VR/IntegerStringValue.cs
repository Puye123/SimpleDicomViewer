using SimpleDicomViewer.Domain.Exceptions;
using System;
using System.Globalization;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// IS(Integer String: 整数列) : string
    /// </summary>
    public class IntegerStringValue : ValueElement
    {
        public IntegerStringValue(Tag tag, byte[] value) : base(tag, value, length: 12, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            // 値の長さチェック
            if (value.Length > Length)
            {
                throw new InvalidDICOMFormatException($"設定可能な値は最大{Length}バイトです。");
            }

            try
            {
                string valueString = System.Text.Encoding.ASCII.GetString(value);
                NumberStyles ns = NumberStyles.Integer;
                IFormatProvider fmt = CultureInfo.InvariantCulture;
                int val;
                bool result = int.TryParse(valueString, ns, fmt, out val);
                if (result)
                {
                    return true;
                }
                else
                {
                    throw new InvalidDICOMFormatException($"設定可能な値は整数を表す文字列です。設定しようとした値は{valueString}です。");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
