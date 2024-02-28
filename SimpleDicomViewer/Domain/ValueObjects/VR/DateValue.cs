using SimpleDicomViewer.Domain.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// DA(Date: 日付) : string
    /// </summary>
    /// <remarks>
    /// 書式 YYYYMMDD の文字列；ここで YYYY は年を含み，MM は月を含み，DD は日を含み，グレゴリオ暦の日付として解釈される。 
    /// 例：”19930822”は 1993 年 8 月 22 日を表す。 
    /// Todo: 範囲照合問合せには現状非対応であり、必要になったら実装する。
    /// </remarks>
    public class DateValue : ValueElement
    {
        public DateValue(Tag tag, byte[] value) : base(tag, value, length: 8, isFixedValue: true, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイトのみです。"); }

            try
            {
                string valueString = System.Text.Encoding.ASCII.GetString(value);
                var result = Regex.IsMatch(valueString, @"^[0-9]* *$");
                if (result)
                {
                    return true;
                }
                else
                {
                    throw new InvalidDICOMFormatException($"設定可能な値は^[0-9]* *$です。設定しようとした値は{valueString}です。");
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
