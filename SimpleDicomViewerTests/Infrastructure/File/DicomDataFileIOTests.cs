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
            var dicomData = dicomDataFileIO.Read("Infrastructure\\File\\US_LEE_IR6.dcm");

            foreach( var ve in dicomData.Values) {
                Console.WriteLine($"{ve.Tag.ToString()} : {ve.GetType()} : {ve.GetValueObject().ToString()}");
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest()
        {
            Assert.Fail();
        }
    }
}