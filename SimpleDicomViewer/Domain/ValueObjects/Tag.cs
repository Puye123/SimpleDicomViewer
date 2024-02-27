namespace SimpleDicomViewer.Domain.ValueObjects
{
    public class Tag : ValueObject<Tag>
    {
        /// <summary>
        /// グループ番号
        /// </summary>
        public ushort GroupNumber { get; }

        /// <summary>
        /// 要素番号
        /// </summary>
        public ushort ElementNumber { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="groupNumber">グループ番号</param>
        /// <param name="elementNumber">要素番号</param>
        public Tag(ushort groupNumber, ushort elementNumber)
        {
            GroupNumber = groupNumber;
            ElementNumber = elementNumber;
        }

        protected override bool EqualCore(Tag other)
        {
            if (GroupNumber == other.GroupNumber && ElementNumber == other.ElementNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
