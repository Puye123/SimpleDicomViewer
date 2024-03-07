using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDicomViewer.Infrastructure.File;
using SimpleDicomViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDicomViewer.ViewModels.Tests
{
    [TestClass()]
    public class DicomListElementTests
    {
        [TestMethod()]
        public void DICOMデータリスト画面に表示するプロパティが取得できているか()
        {
            // Arrange
            DicomDataFileIO dicomDataFileIO = new DicomDataFileIO();
            string expectedPatientName = "HARAJYUKU^ROKUROU";
            string expectedModality = "US";
            string expectedStudyID = "3";
            string expectedSeriesNumber = "1";

            // Act
            // テストデータはここからダウンロード
            // https://www.jira-net.or.jp/dicom/dicom_data_01_03.html 
            var dicomData = dicomDataFileIO.Read("TestData\\US_LEE_IR6.dcm");
            DicomListElement dicomListElement = new DicomListElement(dicomData);

            // Assert
            Assert.AreEqual(expectedPatientName, dicomListElement.PatientName);
            Assert.AreEqual(expectedModality, dicomListElement.Modality);
            Assert.AreEqual(expectedStudyID, dicomListElement.StudyId);
            Assert.AreEqual(expectedSeriesNumber, dicomListElement.SeriesNumber);
        }
    }
}