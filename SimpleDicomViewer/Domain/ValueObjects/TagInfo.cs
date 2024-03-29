namespace SimpleDicomViewer.Domain.ValueObjects
{
    public class TagInfo : ValueObject<TagInfo>
    {
        public string VR { get; }
        public string Description { get; }

        public TagInfo(string VR, string Description)
        {
            this.VR = VR;
            this.Description = Description;
        }

        protected override bool EqualCore(TagInfo other)
        {
            return (this.VR == other.VR && this.Description == other.Description);
        }
    }
}
