using System;
using System.Text.RegularExpressions;
using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    public class AgeStringValue : ValueElement
    {
        public AgeStringValue(Tag tag, byte[] value) : base(tag, value, length: 4, isFixedValue: true, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length != Length) { throw new InvalidDICOMFormatException($"設定可能な値は4バイト固定です。"); }

            try
            {
                string valueString = System.Text.Encoding.ASCII.GetString(value);
                var result = Regex.IsMatch(valueString, @"\d\d\d[DWMY]");
                if (result)
                {
                    return true;
                }
                else
                {
                    throw new InvalidDICOMFormatException($"設定可能な値は\\d\\d\\d[DWMY]です。設定しようとした値は{valueString}です。");
                }
            }
            catch(System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }            
        }
    }
}
