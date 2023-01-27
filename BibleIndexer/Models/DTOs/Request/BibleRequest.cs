namespace BibleIndexer.Models.DTOs.Request
{
    internal class BibleRequest
    {
    }

    public class GetBibleVerseRequest
    {
        public int VerseNumber { get; set; }
        public string BookNameInFull { get; set; }
        public int ChapterNumber { get; set; }
    }
}
