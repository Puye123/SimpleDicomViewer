namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// OW(Other Word String: そのほかのワード列) : byte[]
    /// </summary>
    /// <remarks>
    /// 仕様未実装
    /// 
    /// ※ 仕様上はWord列だが、便宜上Byte列として扱う。問題があれば修正する。
    /// 
    /// 折衝された転送構文によって内容の符号化が指定された16ビットワードの列。
    /// OWはリトルエンディアンとビッグエンディアンの間でバイト順を変更する時，
    /// 各ワード内でバイトスワッピングを必要とするVRである（節7.3参照）。
    /// </remarks>
    public class OtherWordStringValue : ValueElement
    {
        public OtherWordStringValue(Tag tag, byte[] value) : base("OW", tag, value, length: uint.MaxValue, isFixedValue: false, valueType: typeof(byte[]))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            return true;
        }
    }
}
