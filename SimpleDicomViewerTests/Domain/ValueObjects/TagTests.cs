using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDicomViewer.Domain.ValueObjects.Tests
{
    [TestClass()]
    public class TagTests
    {
        [TestMethod()]
        public void プロパティへのアクセス確認()
        {
            // Arrange
            Tag tag = new Tag(groupNumber: 0x1234, elementNumber: 0xabcd);
            ushort expectedGroupNumber = 0x1234;
            ushort expectedElementNumber = 0xabcd;

            // Act

            // Assert
            Assert.AreEqual(expectedGroupNumber, tag.GroupNumber);
            Assert.AreEqual(expectedElementNumber, tag.ElementNumber);
        }

        [TestMethod()]
        [DataRow((ushort)0x1234, (ushort)0xabcd, (ushort)0x1234, (ushort)0xabcd, true)]
        [DataRow((ushort)0x1234, (ushort)0xabcd, (ushort)0x1235, (ushort)0xabcd, false)]
        [DataRow((ushort)0x1234, (ushort)0xabcd, (ushort)0x1234, (ushort)0xabce, false)]
        [DataRow((ushort)0x1234, (ushort)0xabcd, (ushort)0x1235, (ushort)0xabce, false)]
        public void Tag同士の比較(ushort tag1GroupNumber, ushort tag1ElementNumber,
            ushort tag2GroupNumber, ushort tag2ElementNumber, bool expected)
        {
            // Arrange
            Tag tag1 = new Tag(groupNumber: tag1GroupNumber, elementNumber: tag1ElementNumber);
            Tag tag2 = new Tag(groupNumber: tag2GroupNumber, elementNumber: tag2ElementNumber);

            // Act
            bool isEqual = tag1.Equals(tag2);

            // Assert
            Assert.AreEqual(expected, isEqual);
        }
    }
}