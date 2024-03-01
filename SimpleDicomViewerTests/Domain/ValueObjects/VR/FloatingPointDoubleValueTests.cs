using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDicomViewer.Domain.ValueObjects.VR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDicomViewer.Domain.ValueObjects.VR.Tests
{
    [TestClass()]
    public class FloatingPointDoubleValueTests
    {
        [TestMethod()]
        public void 値の長さが正しく設定されているか()
        {
            // Arrange
            var ae = new FloatingPointDoubleValue(new Tag(0x1234, 0xabcd), new byte[] { 0x1f, 0x85, 0xeb, 0x51, 0xb8, 0x1e, 0x09, 0x40 });
            ushort expectedLength = 8;
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
            var ae = new FloatingPointDoubleValue(new Tag(0x1234, 0xabcd), new byte[] { 0x1f, 0x85, 0xeb, 0x51, 0xb8, 0x1e, 0x09, 0x40 });
            double expected = 3.14;

            // Act
            double result = (double)ae.GetValueObject();

            // Assert
            Assert.AreEqual(expected, result, delta: 0.001);
        }
    }
}