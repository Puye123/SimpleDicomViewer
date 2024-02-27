using SimpleDicomViewer.Domain.Exceptions;
using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
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
        public uint Length { get; }

        /// <summary>
        /// 値が固定長かどうか
        /// </summary>
        public bool IsFixedValue { get; }

        /// <summary>
        /// 値
        /// </summary>
        public byte[] Value { get; }

        /// <summary>
        /// 値の型
        /// </summary>
        public Type ValueType { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="isFixedValue"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        protected ValueElement(Tag tag, byte[] value, uint length, bool isFixedValue, Type valueType)
        {
            Length = length;
            IsFixedValue = isFixedValue;
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            ValueType = valueType;

            if (value == null) throw new ArgumentNullException(nameof(value));

            try
            {
                bool result = IsValidValue(value);
                if (result)
                {
                    Value = value;
                }
                else
                {
                    throw new InvalidDICOMFormatException($"不正な設定値です");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 引数で与えられた値が規格として妥当かどうか
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract bool IsValidValue(byte[] value);

        /// <summary>
        /// byte配列を、文字列・数値など扱いやすい型に変換して返す
        /// </summary>
        /// <returns></returns>
        public object GetValueObject()
        {
            if (ValueType == typeof(string))
            {
                return System.Text.Encoding.ASCII.GetString(Value);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
