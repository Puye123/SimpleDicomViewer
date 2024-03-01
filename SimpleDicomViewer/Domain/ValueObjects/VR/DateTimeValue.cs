using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// DT(Date Time: 日時) : string
    /// </summary>
    /// <remarks>
    /// 次の書式の連結日時文字列： YYYYMMDDHHMMSS.FFFFFF&ZZXX
    /// この列の構成要素は，左から右へ YYYY = 年, MM =月, DD = 日, HH = 時（範囲 "00" - "23"），
    /// MM = 分（範囲 "00" -  "59"）， SS = 秒（範囲 "00" -  "60"）。
    /// </remarks>
    public class DateTimeValue : ValueElement
    {
        public DateTimeValue(Tag tag, byte[] value) : base(tag, value, length: 26, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length > Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイト以下です。"); }

            // Todo: 妥当性チェックの実装
            // Todo: DataTimeを表す値オブジェクトを作成すべきか.
            return true;
        }
    }
}
