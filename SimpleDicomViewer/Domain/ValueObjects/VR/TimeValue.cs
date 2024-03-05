using SimpleDicomViewer.Domain.Exceptions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// TM(Time) : string
    /// </summary>
    /// <remarks>
    /// 仕様未実装
    /// </remarks>
    public class TimeValue : ValueElement
    {
        public TimeValue(Tag tag, byte[] value) : base(tag, value, length: 16, isFixedValue: false, valueType: typeof(string))
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
