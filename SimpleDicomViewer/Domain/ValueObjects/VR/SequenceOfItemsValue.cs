namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// SQ(Sequence of Items: 項目のシーケンス) : string
    /// </summary>
    /// <remarks>
    /// 仕様未実装
    /// 
    /// stringではなく、独自の値オブジェクトとしたほうが良いかも 
    /// </remarks>
    public class SequenceOfItemsValue : ValueElement
    {
        public SequenceOfItemsValue(Tag tag, byte[] value) : base(tag, value, length: uint.MaxValue, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            return true;
        }
    }
}
