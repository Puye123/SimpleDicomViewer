using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDicomViewer.Domain.ValueObjects;
using SimpleDicomViewer.Domain.ValueObjects.VR;

namespace SimpleDicomViewerTests.Domain.ValueObjects.VR
{
    [TestClass()]
    public class ApplicationEntityValueTests
    {
        [TestMethod()]
        public void 値の長さが正しく設定されているか()
        {
            // Arrange
            var ae = new ApplicationEntityValue(new Tag(0x1234, 0xabcd), new byte[] { 0x11, 0x12, 0x13 });
            ushort expectedLength = 16;
            bool expectedIsFixedValue = false;

            // Act

            // Assert
            Assert.AreEqual(expectedLength, ae.Length);
            Assert.AreEqual(expectedIsFixedValue, ae.IsFixedValue);
        }

        [TestMethod()]
        public void 文字列型を正しく取得できるか()
        {
            // Arrange
            var ae = new ApplicationEntityValue(new Tag(0x1234, 0xabcd), new byte[] { 0x41, 0x42, 0x43 });
            string expected = "ABC";

            // Act
            object actual = ae.GetValueObject();

            // Assert
            Console.WriteLine(expected + actual);
            Assert.AreEqual(expected, actual);
        }
    }
}