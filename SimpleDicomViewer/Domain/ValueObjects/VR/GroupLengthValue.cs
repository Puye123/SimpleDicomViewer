namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// Group Lengthタグ(gggg, 0000) を表すクラス
    /// </summary>
    /// <remarks>
    /// 実際はVRの一種ではない。
    /// </remarks>
    public class GroupLengthValue : ValueElement
    {
        public GroupLengthValue(Tag tag, byte[] value) : base("Group Length", tag, value, length : int.MaxValue, isFixedValue : false, valueType : typeof(uint))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            return true;
        }
    }
}
