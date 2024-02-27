using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDicomViewer.Domain.Exceptions;
using SimpleDicomViewer.Domain.ValueObjects.VR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDicomViewer.Domain.ValueObjects.VR.Tests
{
    [TestClass()]
    public class AgeStringValueTests
    {
        [TestMethod()]
        public void 値の長さが正しく設定されているか()
        {
            // Arrange
            var ae = new AgeStringValue(new Tag(0x1234, 0xabcd), new byte[] { 0x30, 0x31, 0x32, 0x4d });
            ushort expectedLength = 4;
            bool expectedIsFixedValue = true;

            // Act

            // Assert
            Assert.AreEqual(expectedLength, ae.Length);
            Assert.AreEqual(expectedIsFixedValue, ae.IsFixedValue);
        }

        [TestMethod()]
        [DataRow("012D")]
        [DataRow("345W")]
        [DataRow("678Y")]
        [DataRow("901M")]
        public void 正規文字列の場合(string input)
        {
            // Arrange
            var byteArray = Encoding.ASCII.GetBytes(input);
            var ae = new AgeStringValue(new Tag(0x1234, 0xabcd), byteArray);

            // Act
            var result = ae.GetValueObject();

            // Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod()]
        [DataRow("012A")]
        [DataRow("A45W")]
        [DataRow("6A8Y")]
        [DataRow("90AM")]
        [DataRow("90M")]
        [DataRow("1234M")]
        [ExpectedException(typeof(InvalidDICOMFormatException))]
        public void 不正文字列の場合(string input)
        {
            // Arrange
            var byteArray = Encoding.ASCII.GetBytes(input);
            var ae = new AgeStringValue(new Tag(0x1234, 0xabcd), byteArray);
            // 不正な入力なので、↑のインスタンス生成時に例外が発生することを期待

            // Act
           
            // Assert
        }
    }
}