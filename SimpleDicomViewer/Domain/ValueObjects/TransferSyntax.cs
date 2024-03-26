namespace SimpleDicomViewer.Domain.ValueObjects
{
    public enum TransferSyntaxName {
        Undifine = 0,

        ImplicitVREndian = 1,
        ExplicitVRLittleEndian,
        DeflatedExplicitVRLittleEndian,
        ExplicitVRBigEndian,

        JPEGBaselineProcess1,
        JPEGBaselineProcess2and4,
        JPEGLosslessProcess14,
        JPEGLosslessProcess14SelectionValue1,
        JPEG_LSLossless,
        JPEG_LSLossy,
        JPEG2000LosslessOnly,
        JPEG2000,
        JPEG2000Part2LosslessOnly,
        JPEG2000Part2,

        JPIPReferenced,
        JPIPReferencedDeflate,
        RLELossless,
        RFC2557MIMEEncapsulation,

        MPEG2,
        MPEG4HighProfile,
        MPEG4BDcompatibleHighProfile,
    }


    public class TransferSyntax : ValueObject<TransferSyntax>
    {
        public string TransferSyntaxUID { get; }
        public TransferSyntaxName TransferSyntaxName { get; }
        public TransferSyntax(string transferSyntaxUID) {
            TransferSyntaxUID = transferSyntaxUID;
            TransferSyntaxName = transferSyntaxUID switch
            {
                "1.2.840.10008.1.2" => TransferSyntaxName.ImplicitVREndian,
                "1.2.840.10008.1.2.1" => TransferSyntaxName.ExplicitVRLittleEndian,
                "1.2.840.10008.1.2.1.99" => TransferSyntaxName.DeflatedExplicitVRLittleEndian,
                "1.2.840.10008.1.2.2" => TransferSyntaxName.ExplicitVRBigEndian,

                "1.2.840.10008.1.2.4.50" => TransferSyntaxName.JPEGBaselineProcess1,
                "1.2.840.10008.1.2.4.51" => TransferSyntaxName.JPEGBaselineProcess2and4,
                "1.2.840.10008.1.2.4.57" => TransferSyntaxName.JPEGLosslessProcess14,
                "1.2.840.10008.1.2.4.70" => TransferSyntaxName.JPEGLosslessProcess14SelectionValue1,
                "1.2.840.10008.1.2.4.80" => TransferSyntaxName.JPEG_LSLossless,
                "1.2.840.10008.1.2.4.81" => TransferSyntaxName.JPEG_LSLossy,
                "1.2.840.10008.1.2.4.90" => TransferSyntaxName.JPEG2000LosslessOnly,
                "1.2.840.10008.1.2.4.91" => TransferSyntaxName.JPEG2000,
                "1.2.840.10008.1.2.4.92" => TransferSyntaxName.JPEG2000Part2LosslessOnly,
                "1.2.840.10008.1.2.4.93" => TransferSyntaxName.JPEG2000Part2,

                "1.2.840.10008.1.2.4.94" => TransferSyntaxName.JPIPReferenced,
                "1.2.840.10008.1.2.4.95" => TransferSyntaxName.JPIPReferencedDeflate,
                "1.2.840.10008.1.2.5" => TransferSyntaxName.RLELossless,
                "1.2.840.10008.1.2.6.1" => TransferSyntaxName.RFC2557MIMEEncapsulation,

                "1.2.840.10008.1.2.4.100" => TransferSyntaxName.MPEG2,
                "1.2.840.10008.1.2.4.102" => TransferSyntaxName.MPEG4HighProfile,
                "1.2.840.10008.1.2.4.103" => TransferSyntaxName.MPEG4BDcompatibleHighProfile,

                _ => TransferSyntaxName.Undifine,
            };
        }
        
        protected override bool EqualCore(TransferSyntax other)
        {
            return other.TransferSyntaxName == TransferSyntaxName;
        }
    }
}
