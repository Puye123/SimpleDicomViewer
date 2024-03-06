namespace SimpleDicomViewer.Domain.ValueObjects
{
    public enum TagType
    {
        Standard = 0,
        Private = 1
    }

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
        /// タグの属性
        /// </summary>
        public TagType TagType { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="groupNumber">グループ番号</param>
        /// <param name="elementNumber">要素番号</param>
        public Tag(ushort groupNumber, ushort elementNumber)
        {
            GroupNumber = groupNumber;
            ElementNumber = elementNumber;

            if (GroupNumber % 2 == 0)
            {
                TagType = TagType.Standard;
            }
            else
            {
                TagType = TagType.Private;
            }
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

        public override string ToString()
        {
            return $"({GroupNumber:X4}, {ElementNumber:X4})";
        }
    }
}
