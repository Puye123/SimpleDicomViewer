using SimpleDicomViewer.Domain.Exceptions;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// DS(Decimal String: 10進数列) : string
    /// </summary>
    /// <remarks>
    /// 固定小数点か浮動小数点数を表現する文字列。固定小数点数は文字 0-9，任意の先頭の “+” または “-”，および小数点を示す任意の “.” のみを含む。
    /// 浮動小数点数は，ANSI X3.9 の中で定義されるとおり，指数の始まりを示す “E” か “e” を持って伝達される。
    /// 10進数列は先頭あるいは末尾スペースで埋められることがある。
    /// 埋込まれた（途中の）スペースは許されない。
    /// </remarks>
    public class DecimalStringValue : ValueElement
    {
        public DecimalStringValue(Tag tag, byte[] value) : base(tag, value, length: 16, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length > Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイト以下です。"); }

            try
            {
                string valueString = System.Text.Encoding.ASCII.GetString(value);
                NumberStyles ns = NumberStyles.AllowExponent | NumberStyles.Float | NumberStyles.Integer;
                IFormatProvider fmt = CultureInfo.InvariantCulture;

                bool result = double.TryParse(valueString, ns, fmt, out double val);
                if (result)
                {
                    return true;
                }
                else
                {
                    throw new InvalidDICOMFormatException($"設定可能な値は10進数を表す文字列です。設定しようとした値は{valueString}です。");
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
