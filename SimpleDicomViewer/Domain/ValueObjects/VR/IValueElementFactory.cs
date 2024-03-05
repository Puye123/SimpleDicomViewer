namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    public interface IValueElementFactory
    {
        /// <summary>
        /// ValueElementを生成する
        /// </summary>
        /// <param name="vrString">"AS", "AT" などといったVRを表す文字列</param>
        /// <param name="tag">タグ</param>
        /// <param name="value">値</param>
        /// <returns>ValueElement</returns>
        public ValueElement Create(string vrString, Tag tag, byte[] value);
    }
}
