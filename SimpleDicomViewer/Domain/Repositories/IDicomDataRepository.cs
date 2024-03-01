using SimpleDicomViewer.Domain.Entities;

namespace SimpleDicomViewer.Domain.Repositories
{
    public interface IDicomDataRepository
    {
        /// <summary>
        /// DICOMデータの読み込み
        /// </summary>
        /// <param name="pathString">読み込み元のパス文字列</param>
        /// <returns>DICOMデータエンティティ</returns>
        public DicomDataEntity Read(string pathString);

        /// <summary>
        /// DICOMデータの書き込み
        /// </summary>
        /// <param name="pathString">書き込み先のパス文字列</param>
        /// <param name="dicomData">DICOMデータエンティティ</param>
        public void Write(string pathString, DicomDataEntity dicomData);
    }
}
