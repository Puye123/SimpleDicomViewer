using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDicomViewer.Domain.Exceptions;
using System.Text;

namespace SimpleDicomViewer.Domain.ValueObjects.VR.Tests
{
    [TestClass()]
    public class CodeStringValueTests
    {
        [TestMethod()]
        [DataRow("012ABC _")]
        [DataRow("ABCDEFGHIJKIJKL")]
        [DataRow("   ABCD    ")]
        [DataRow("")]
        public void 正規文字列の場合(string input)
        {
            // Arrange
            var byteArray = Encoding.ASCII.GetBytes(input);
            var ae = new CodeStringValue(new Tag(0x1234, 0xabcd), byteArray);

            // Act
            var result = ae.GetValueObject();

            // Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod()]
        [DataRow("012abc _")]
        [DataRow("ABCDEFG*IJKIJKL")]
        [DataRow("   ABCD+    ")]
        [ExpectedException(typeof(InvalidDICOMFormatException))]
        public void 不正文字列の場合(string input)
        {
            // Arrange
            var byteArray = Encoding.ASCII.GetBytes(input);
            var ae = new CodeStringValue(new Tag(0x1234, 0xabcd), byteArray);
            // 不正な入力なので、↑のインスタンス生成時に例外が発生することを期待

            // Act

            // Assert
        }
    }
}