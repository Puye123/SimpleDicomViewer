using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// SQ関連のデータ要素
    /// </summary>
    /// <remarks>
    /// * アイテム (FFFE, E0000)
    /// * アイテム区切りアイテム (FFFE, E00D)
    /// * シーケンス区切りアイテム (FFFE, E0DD)
    /// </remarks>
    public class SequenceDelimitationItemValue : ValueElement
    {
        public SequenceDelimitationItemValue(string vr, Tag tag) : base(vr, tag, value : Array.Empty<byte>(), length : 0, isFixedValue : false, valueType : typeof(byte[]))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            return true;
        }
    }
}
