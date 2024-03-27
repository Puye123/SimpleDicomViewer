using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    public class ValueElementFactory : IValueElementFactory
    {
        /// <summary>
        /// ValueElementを生成する
        /// </summary>
        /// <param name="vrString">"AS", "AT" などといったVRを表す文字列</param>
        /// <param name="tag">タグ</param>
        /// <param name="value">値</param>
        /// <returns>ValueElement</returns>
        public ValueElement Create(string vrString, Tag tag, byte[] value)
        {
            if (vrString == null) throw new ArgumentNullException(nameof(vrString));
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            if (value == null) throw new ArgumentNullException(nameof(value));

            return vrString switch
            {
                "AS" => new AgeStringValue(tag, value),
                "AE" => new ApplicationEntityValue(tag, value),
                "AT" => new AttributeTagValue(tag, value),
                "CS" => new CodeStringValue(tag, value),
                "DT" => new DateTimeValue(tag, value),
                "DA" => new DateValue(tag, value),
                "DS" => new DecimalStringValue(tag, value),
                "FD" => new FloatingPointDoubleValue(tag, value),
                "FL" => new FloatingPointSingleValue(tag, value),
                "IS" => new IntegerStringValue(tag, value),
                "LO" => new LongStringValue(tag, value),
                "LT" => new LongTextValue(tag, value),
                "OB" => new OtherByteStringValue(tag, value),
                "OF" => new OtherFloatStringValue(tag, value),
                "OW" => new OtherWordStringValue(tag, value),
                "PN" => new PersonNameValue(tag, value),
                "SH" => new ShortStringValue(tag, value),
                "SL" => new SignedLongValue(tag, value),
                "SQ" => new SequenceOfItemsValue(tag, value),
                "SS" => new SignedShortValue(tag, value),
                "ST" => new ShortTextValue(tag, value),
                "TM" => new TimeValue(tag, value),
                "UI" => new UNlimitedTextValue(tag, value),
                "UL" => new UnsignedLongValue(tag, value),
                "UN" => new UnknownValue(tag, value),
                "US" => new UnsignedShortValue(tag, value),
                "UT" => new UNlimitedTextValue(tag, value),
                _ => throw new InvalidDICOMFormatException($"読み込みに失敗しました。暗黙的VRを含むデータには対応していません。タグ:  {vrString.ToString()}"),
            };
        }
    }
}
