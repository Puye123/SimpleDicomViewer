using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// AE(Application Entity: 応用エンティティ) : string
    /// </summary>
    /// <remarks>
    /// 意味のない先頭と末尾のSPACE(20H)を持つ、応用エンティティを識別する文字列。
    /// スペ－スのみで構成される値は使用しない。
    /// </remarks>
    public class ApplicationEntityValue : ValueElement
    {
        public ApplicationEntityValue(Tag tag, byte[] value) : base(tag, value, length: 16, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            // 値の長さチェック
            if (value.Length > Length)
            {
                throw new InvalidDICOMFormatException($"設定可能な値は最大{Length}バイトです。");
            }

            // 文字レパートリチェック
            if (Array.Exists(value, x => x == 0x5c || x == 0x0a || x == 0x0c || x == 0x0d || x == 0x1b))
            {
                throw new InvalidDICOMFormatException($"不正な文字が含まれます");
            }

            return true;
        }
    }
}
