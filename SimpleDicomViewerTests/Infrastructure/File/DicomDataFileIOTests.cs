using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDicomViewer.Infrastructure.File.Tests
{
    [TestClass()]
    public class DicomDataFileIOTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            DicomDataFileIO dicomDataFileIO = new DicomDataFileIO();

            // テストデータはここからダウンロード
            // https://www.jira-net.or.jp/dicom/dicom_data_01_03.html 
            dicomDataFileIO.Read("Infrastructure\\File\\US_LEE_IR6.dcm");
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest()
        {
            Assert.Fail();
        }
    }
}