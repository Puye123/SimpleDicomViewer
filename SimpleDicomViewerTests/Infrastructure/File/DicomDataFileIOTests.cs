using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDicomViewer.Infrastructure.File.Tests
{
    [TestClass()]
    public class DicomDataFileIOTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            // Arrange
            DicomDataFileIO dicomDataFileIO = new DicomDataFileIO();
            int expectedElementCount = 38; // 別のビュワーアプリで確認した期待値

            // Act
            // テストデータはここからダウンロード
            // https://www.jira-net.or.jp/dicom/dicom_data_01_03.html 
            var dicomData = dicomDataFileIO.Read("TestData\\US_LEE_IR6.dcm");

            foreach( var ve in dicomData.Values) {
                Console.WriteLine($"{ve.Tag.ToString()} : {ve.GetType()} : {ve.GetValueObject().ToString()}");
            }

            // Assert
            Assert.AreEqual(expectedElementCount, dicomData.Values.Count);
        }
    }
}