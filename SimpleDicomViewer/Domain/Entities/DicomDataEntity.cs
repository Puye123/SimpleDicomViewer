using SimpleDicomViewer.Domain.ValueObjects.VR;
using System.Collections.Generic;

namespace SimpleDicomViewer.Domain.Entities
{
    public class DicomDataEntity
    {
        // DICOMデータ要素のリスト
        public List<ValueElement> Values { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DicomDataEntity(List<ValueElement> valueElements) {
            this.Values = valueElements;
        }
    }
}
