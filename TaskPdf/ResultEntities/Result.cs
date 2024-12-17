namespace TaskPdf.ResultEntities
{
    public class Result
    {
        public List<FileInformation> Files { get; set; }
        public Dictionary<string, int> TotalKeywordCounts { get; set; }
    }
}
