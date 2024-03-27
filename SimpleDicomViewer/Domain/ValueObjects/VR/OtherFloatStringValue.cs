using System;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// OF(Other Float String)
    /// </summary>
    /// <remarks>
    /// 仕様未実装
    /// 
    /// 32ビットIEEE 754:1985浮動小数点ワードの列。OFは，リトルエンディアンとビッグエンディアンの間でバイト順を変更する時，各32ビットワード内でバイトスワッピングを必要とするVRである（節7.3参照）。 
    /// </remarks>
    public class OtherFloatStringValue : ValueElement
    {
        public OtherFloatStringValue(Tag tag, byte[] value) : base("OF", tag, value, length: (uint)Math.Pow(2, 32) - 4, isFixedValue: false, valueType: typeof(float))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            throw new NotImplementedException();
        }
    }
}
