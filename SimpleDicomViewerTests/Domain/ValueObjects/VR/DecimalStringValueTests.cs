using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDicomViewer.Domain.Exceptions;
using System.Text;

namespace SimpleDicomViewer.Domain.ValueObjects.VR.Tests
{
    [TestClass()]
    public class DecimalStringValueTests
    {
        [TestMethod()]
        public void 値の長さが正しく設定されているか()
        {
            // Arrange
            var ae = new DecimalStringValue(new Tag(0x1234, 0xabcd), new byte[] { 0x30, 0x31, 0x32, 0x30, 0x31, 0x32, 0x30, 0x31 });
            ushort expectedLength = 16;
            bool expectedIsFixedValue = false;

            // Act

            // Assert
            Assert.AreEqual(expectedLength, ae.Length);
            Assert.AreEqual(expectedIsFixedValue, ae.IsFixedValue);
        }

        [TestMethod()]
        [DataRow("123456789")]
        [DataRow("+123")]
        [DataRow("-123")]
        [DataRow("-123e4")]
        [DataRow("-123e-4")]
        [DataRow("-123E4")]
        [DataRow("-123E-4")]
        [DataRow(" -123e-4")]
        [DataRow("-123e-4 ")]
        [DataRow(" -123e-4 ")]
        public void 正規文字列の場合(string input)
        {
            // Arrange
            var byteArray = Encoding.ASCII.GetBytes(input);
            var ae = new DecimalStringValue(new Tag(0x1234, 0xabcd), byteArray);

            // Act
            var result = ae.GetValueObject();

            // Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod()]
        [DataRow("12345678912345678")]
        [DataRow("*123")]
        [DataRow("12a")]
        [DataRow("-123f4")]
        [DataRow("-12 3e-4")]
        [ExpectedException(typeof(InvalidDICOMFormatException))]
        public void 不正文字列の場合(string input)
        {
            // Arrange
            var byteArray = Encoding.ASCII.GetBytes(input);
            var ae = new DecimalStringValue(new Tag(0x1234, 0xabcd), byteArray);
            // 不正な入力なので、↑のインスタンス生成時に例外が発生することを期待

            // Act

            // Assert
        }
    }
}