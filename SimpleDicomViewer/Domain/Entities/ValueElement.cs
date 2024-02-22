using SimpleDicomViewer.Domain.ValueObjects;
using System;

namespace SimpleDicomViewer.Domain.Entities
{
    public abstract class ValueElement
    {
        /// <summary>
        /// タグ
        /// </summary>
        public Tag Tag { get; }

        /// <summary>
        /// 値の長さ or 最大長
        /// </summary>
        /// <remarks>
        /// 代入は派生クラスのコンストラクタで行うとよい
        /// </remarks>
        public uint Length { get; protected set; }

        /// <summary>
        /// 値が固定長かどうか
        /// </summary>
        /// <remarks>
        /// 代入は派生クラスのコンストラクタで行うとよい
        /// </remarks>
        public bool isFixedValue { get; protected set; }

        /// <summary>
        /// 値
        /// </summary>
        public byte[] Value { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tag">タグ</param>
        /// <param name="value">データ値</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        protected ValueElement(Tag tag, byte[] value)
        {
            if (Tag == null) throw new ArgumentNullException(nameof(value));
            Tag = tag;

            if (value == null) throw new ArgumentNullException(nameof(value));
            if (!IsValidValue(value)) throw new ArgumentException("DICOM仕様に反したデータフォーマットです", nameof(value));
            Value = value;
        }

        /// <summary>
        /// 引数で与えられた値が規格として妥当かどうか
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool IsValidValue(byte[] value);
    }
}
