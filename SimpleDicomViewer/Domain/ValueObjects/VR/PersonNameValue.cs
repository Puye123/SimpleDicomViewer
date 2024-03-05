namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// PN(Person Name: 人名) : string
    /// </summary>
    /// <remarks>
    /// 仕様未実装
    /// 
    /// Todo: string型ではなく、独自の値オブジェクトとして持たせるべきか。
    /// 構成要素の区切り処理等はドメイン側で完結すべき。
    /// </remarks>
    public class PersonNameValue : ValueElement
    {
        public PersonNameValue(Tag tag, byte[] value) : base(tag, value, length: uint.MaxValue, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            return true;
        }
    }
}
