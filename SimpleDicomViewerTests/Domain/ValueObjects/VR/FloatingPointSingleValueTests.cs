using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDicomViewer.Domain.ValueObjects.VR.Tests
{
    [TestClass()]
    public class FloatingPointSingleValueTests
    {
        [TestMethod()]
        public void 値の長さが正しく設定されているか()
        {
            // Arrange
            var ae = new FloatingPointSingleValue(new Tag(0x1234, 0xabcd), new byte[] { 0x40, 0x48, 0xf5, 0xc3 });
            ushort expectedLength = 4;
            bool expectedIsFixedValue = true;

            // Act

            // Assert
            Assert.AreEqual(expectedLength, ae.Length);
            Assert.AreEqual(expectedIsFixedValue, ae.IsFixedValue);
        }


        [TestMethod()]
        public void 正規文字列の場合()
        {
            // Arrange
            var ae = new FloatingPointSingleValue(new Tag(0x1234, 0xabcd), new byte[] { 0xc3, 0xf5, 0x48, 0x40 });
            float expected = 3.14F;

            // Act
            float result = (float)ae.GetValueObject();

            // Assert
            Assert.AreEqual(expected, result, delta: 0.001);
        }
    }
}