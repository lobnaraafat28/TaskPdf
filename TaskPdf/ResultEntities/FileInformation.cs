using Org.BouncyCastle.Asn1.Cms;

namespace TaskPdf.ResultEntities
{
    public class FileInformation
    {
        public string FileName { get; set; }
        public Metadata Metadata { get; set; }
        public Dictionary<string, int> KeywordCounts { get; set; }
    }
}
