using SimpleDicomViewer.Domain.Exceptions;
using System;
using System.Linq;

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

        public string VRName { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="isFixedValue"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        protected ValueElement(string vr, Tag tag, byte[] value, uint length, bool isFixedValue, Type valueType)
        {
            VRName = vr;
            Length = length;
            IsFixedValue = isFixedValue;
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            ValueType = valueType;

            if (value == null) throw new ArgumentNullException(nameof(value));

            try
            {
                // ワークアラウンド
                // 多重値(バックスラッシュ)は値チェックしない
                if (Array.Exists(value, x => x == 0x5c))
                {
                    Value = value;
                    return;
                }

                bool result = IsValidValue(value);
                // Todo: 値チェックは例外のみで行うか検討
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
            if (Value == null || Value.Length == 0)
            {
                return "";
            }
            if (ValueType == typeof(string))
            {
                return System.Text.Encoding.ASCII.GetString(Value);
            }
            if (ValueType == typeof(float))
            {
                return BitConverter.ToSingle(Value);
            }
            if (ValueType == typeof(double))
            {
                return BitConverter.ToDouble(Value);
            }
            if (ValueType == typeof(byte[]))
            {
                return Value;
            }
            if (ValueType == typeof(int))
            {
                return BitConverter.ToInt32(Value);
            }
            if (ValueType == typeof(short))
            {
                return BitConverter.ToInt16(Value);
            }
            if (ValueType == typeof(uint))
            {
                return BitConverter.ToUInt32(Value);
            }
            if (ValueType == typeof(ushort))
            {
                return BitConverter.ToUInt16(Value);
            }
            if (ValueType == typeof(Tag))
            {
                ushort group = BitConverter.ToUInt16(Value.Take(2).ToArray()); // 上位2バイト
                ushort element = BitConverter.ToUInt16(Value.Skip(2).ToArray()); // 下位2バイト
                Tag tag = new(group, element);
                return tag;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return VRName;
        }
    }
}
