using BibleIndexer.Models.DTOs.Request;
using BibleIndexer.Models.DTOs.Response;

namespace BibleIndexer.Services.Interfaces
{
    public interface IBibleService
    {
        Task<BibleVerseResponse?> GenerateRandomBibleVerse();
        Task<object> GetAllBooksOfTheBible();
        Task<VersesResponse?> GetAllVersesInAChapterOFTheBible(GetBibleVerseRequest request);
        Task<BibleVerseResponse?> GetBibleVerse(GetBibleVerseRequest request);
        Task<BlobResponse?> GetBookOfTheBible(string bookName);
        Task<ChaptersResponse?> GetChaptersInABookOfTheBible(string name);
        Task<IEnumerable<BibleVerseResponse>> SearchBible(string query);
    }
}