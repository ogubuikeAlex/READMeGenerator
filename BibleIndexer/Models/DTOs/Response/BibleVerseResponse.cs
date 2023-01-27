namespace BibleIndexer.Models.DTOs.Response
{
    public record BibleVerseResponse
    {
        public int VerseNumber { get; set; }
        public string VerseContent { get; set; }
        public int ChapterNumber { get; set; }
        public int BookNumber { get; set; }
        public string BookName { get; set; }
    }

    public record ChapterResonse
    {
        public string BookName { get; set; }
        public List<ChapterResonse> Chapters { get; set; }
    }

    public record BlobResponse
    {
        public string Abbrev { get; set; }
        public string Name { get; set; }
        public List<List<string>> Chapters { get; set; }
    }

    public record ChaptersResponse : ResourseAndDropdownResponse<List<string>>
    {
    }

    public record VersesResponse : ResourseAndDropdownResponse<string>
    {
    }

    public record ResourseAndDropdownResponse<T>
    {
        public string BookName { get; set; }
        public IEnumerable<dynamic> DropDown { get; set; }
        public IEnumerable<T> Resource { get; set; }
    }
}
