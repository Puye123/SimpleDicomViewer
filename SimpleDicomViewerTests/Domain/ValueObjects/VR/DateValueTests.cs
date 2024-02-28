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
    public class DateValueTests
    {
        [TestMethod()]
        public void 値の長さが正しく設定されているか()
        {
            // Arrange
            var ae = new DateValue(new Tag(0x1234, 0xabcd), new byte[] { 0x30, 0x31, 0x32, 0x30, 0x31, 0x32, 0x30, 0x31 });
            ushort expectedLength = 8;
            bool expectedIsFixedValue = true;

            // Act

            // Assert
            Assert.AreEqual(expectedLength, ae.Length);
            Assert.AreEqual(expectedIsFixedValue, ae.IsFixedValue);
        }

        [TestMethod()]
        [DataRow("19900423")]
        [DataRow("20210404")]
        [DataRow("20231219")]
        public void 正規文字列の場合(string input)
        {
            // Arrange
            var byteArray = Encoding.ASCII.GetBytes(input);
            var ae = new DateValue(new Tag(0x1234, 0xabcd), byteArray);

            // Act
            var result = ae.GetValueObject();

            // Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod()]
        [DataRow("199a0423")]
        [DataRow("202144")]
        [DataRow("2023 219")]
        [ExpectedException(typeof(InvalidDICOMFormatException))]
        public void 不正文字列の場合(string input)
        {
            // Arrange
            var byteArray = Encoding.ASCII.GetBytes(input);
            var ae = new DateValue(new Tag(0x1234, 0xabcd), byteArray);
            // 不正な入力なので、↑のインスタンス生成時に例外が発生することを期待

            // Act

            // Assert
        }
    }
}