using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SimpleDicomViewer.Domain.Entities;

namespace SimpleDicomViewer.ViewModels
{
    /// <summary>
    /// DICOMデータリスト表示用クラス
    /// </summary>
    public partial class DicomListElement : ObservableObject
    {
        /// <summary>
        /// 患者名
        /// </summary>
        [ObservableProperty]
        string patientName = "??";

        /// <summary>
        /// モダリティ
        /// </summary>
        [ObservableProperty]
        string modality = "??";

        /// <summary>
        /// Study ID
        /// </summary>
        [ObservableProperty]
        string studyId = "??";

        /// <summary>
        /// シリーズ番号
        /// </summary>
        [ObservableProperty]
        string seriesNumber = "??";

        /// <summary>
        /// Domainエンティティを保持しておく
        /// </summary>
        DicomDataEntity DicomDataEntity { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dicomDataEntity"></param>
        public DicomListElement(DicomDataEntity dicomDataEntity)
        {
            DicomDataEntity = dicomDataEntity;

            var patientNameElement = DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0010, 0x0010)).FirstOrDefault();
            PatientName = patientNameElement?.GetValueObject().ToString().Trim();

            var modalityElement = DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0008, 0x0060)).FirstOrDefault();
            Modality = modalityElement?.GetValueObject().ToString().Trim();

            var studyIdElement = DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0020, 0x0010)).FirstOrDefault();
            StudyId = studyIdElement?.GetValueObject().ToString().Trim();

            var seriesNumberElement = DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0020, 0x0011)).FirstOrDefault();
            SeriesNumber = seriesNumberElement?.GetValueObject().ToString().Trim();
        }
    }
}
