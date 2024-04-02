using SimpleDicomViewer.Domain.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
        /// 多重値だったときに各々の値を格納するリスト
        /// </summary>
        private List<byte[]> ValueList { get; set; } = new List<byte[]>();

        /// <summary>
        /// 値の型
        /// </summary>
        public Type ValueType { get;}

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
                // 多重値(バックスラッシュ区切り)  画像データはのぞく
                if (Array.Exists(value, x => x == 0x5c) && Tag != new Tag(0x7fe0, 0x0010))
                {
                    Value = value;

                    List<byte> currentList = new List<byte>();
                    foreach (var v in value)
                    {
                        if (v == 0x5c)
                        {
                            if (currentList.Count > 0)
                            {
                                ValueList.Add(currentList.ToArray());
                                currentList = new List<byte>();
                            }
                        }
                        else
                        {
                            currentList.Add(v);
                        }
                    }
                    if (currentList.Count > 0)
                    {
                        ValueList.Add(currentList.ToArray());
                    }
                    ValueType = ValueType.MakeArrayType(); // 型名情報を配列型に変換
                    return;
                }
                // 多重値(区切り文字なし)
                if (value.Length > length && isFixedValue == true && value.Length % length == 0)
                {
                    Value = value;

                    for (int i = 0; i < value.Length; i += (int)length)
                    {
                        ValueList.Add(new List<byte>(value.Skip(i).Take((int)length)).ToArray());
                    }
                    ValueType = ValueType.MakeArrayType(); // 型名情報を配列型に変換
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
            if (ValueType == typeof(string[]))
            {
                return ValueList.ConvertAll(x => System.Text.Encoding.ASCII.GetString(x)).ToArray();
            }
            if (ValueType == typeof(float))
            {
                return BitConverter.ToSingle(Value);
            }
            if (ValueType == typeof(float[]))
            {
                return ValueList.ConvertAll(x => BitConverter.ToSingle(x)).ToArray();
            }
            if (ValueType == typeof(double))
            {
                return BitConverter.ToDouble(Value);
            }
            if (ValueType == typeof(double[]))
            {
                return ValueList.ConvertAll(x => BitConverter.ToDouble(x)).ToArray();
            }
            if (ValueType == typeof(byte[]))
            {
                return Value;
            }
            if (ValueType == typeof(int))
            {
                return BitConverter.ToInt32(Value);
            }
            if (ValueType == typeof(int[]))
            {
                return ValueList.ConvertAll(x => BitConverter.ToInt32(x)).ToArray();
            }
            if (ValueType == typeof(short))
            {
                return BitConverter.ToInt16(Value);
            }
            if (ValueType == typeof(short[]))
            {
                return ValueList.ConvertAll(x => BitConverter.ToInt16(x)).ToArray();
            }
            if (ValueType == typeof(uint))
            {
                return BitConverter.ToUInt32(Value);
            }
            if (ValueType == typeof(uint[]))
            {
                return ValueList.ConvertAll(x => BitConverter.ToUInt32(x)).ToArray();
            }
            if (ValueType == typeof(ushort))
            {
                return BitConverter.ToUInt16(Value);
            }
            if (ValueType == typeof(ushort[]))
            {
                return ValueList.ConvertAll(x => BitConverter.ToUInt16(x)).ToArray();
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
                throw new NotImplementedException($"未定義の型です : {ValueType}");
            }
        }

        public override string ToString()
        {
            return VRName;
        }
    }
}
