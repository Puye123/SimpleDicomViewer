using System;

namespace SimpleDicomViewer.Domain.ValueObjects
{
    public enum PhotometricInterpretationName
    {
        RGB = 0,
        MONOCHROME1 = 1,
        MONOCHROME2 = 2,

        OTHER = 99
    }

    public class PhotometricInterpretation : ValueObject<PhotometricInterpretation>
    {
        public PhotometricInterpretationName Name { get; }

        public PhotometricInterpretation(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (name == "RGB")
            {
                Name = PhotometricInterpretationName.RGB;
            }
            else if (name == "MONOCHROME1")
            {
                Name = PhotometricInterpretationName.MONOCHROME1;
            }
            else if (name == "MONOCHROME2")
            {
                Name = PhotometricInterpretationName.MONOCHROME2;
            }
            else
            {
                throw new ArgumentException(nameof(name));
            }
        }

        protected override bool EqualCore(PhotometricInterpretation other)
        {
            throw new NotImplementedException();
        }
    }
}
