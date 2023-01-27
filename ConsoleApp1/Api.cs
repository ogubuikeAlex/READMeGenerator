using Newtonsoft.Json;
using RestSharp;
using BibleIndexer.Models.DTOs.Request;
using BibleIndexer.Models.DTOs.Response;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    public class AZA
    {
        const int bibleBookCount = 66;
        const int first = 1;
        public static string letter;

        public async static Task<IEnumerable<dynamic>?> GetBlob() => bibleBlob = bibleBlob == null ? await GetBibleBlob() : bibleBlob ?? null;

        public async static Task<string> GenerateRandomBibleVerse()
        {
            //Generate a number from 0 - 65
            var randomBibleBookIndex = RandomNumberGenerator.GetInt32(bibleBookCount);

            //From that number get the bible name and number of chapters
            IEnumerable<dynamic> bibleBlob = await GetBibleBlob();
            var book = bibleBlob.ElementAt(randomBibleBookIndex);
            if (book is null) return string.Empty;

            var bookName = book.Name;
            var chapterCount = book.Chapters.Count;

            //Using the number of chapters Generate a random number
            int randomBibleChapterIndex = RandomNumberGenerator.GetInt32(chapterCount);
            //For that chapter get the number of verses and generate another random number
            var chapter = JsonConvert.DeserializeObject<List<List<string>>>(Convert.ToString(book.Chapters));

            var verseCount = chapter.Count;
            var randomVerseIndex = RandomNumberGenerator.GetInt32(verseCount);
            return await GetBibleVerse(new() { BookNameInFull = bookName, ChapterNumber = randomBibleChapterIndex, VerseNumber = randomVerseIndex });
        }
        public static async Task<string> GetBibleVerse(GetBibleVerseRequest request)
        {
            BlobResponse? bookResponse = await GetBookOfTheBible(request.BookNameInFull);
            if (bookResponse is null) return string.Empty;
            return bookResponse.Chapters[request.ChapterNumber - first][request.VerseNumber - first];
        }
        public static async Task<BlobResponse?> GetBookOfTheBible(string bookName)
        {
            IEnumerable<dynamic>? bibleBlob = await GetBibleBlob();

            if (!bibleBlob.Any()) return null;
            dynamic? result = bibleBlob.FirstOrDefault(x => Convert.ToString(x.Name).ToLower() == bookName.ToLower().Trim());

            if (result is null) return null;
            return JsonConvert.DeserializeObject<BlobResponse>(Convert.ToString(result));
        }

        public async static Task<ChaptersResponse?> GetChaptersInABookOfTheBible(string name)
        {
            IEnumerable<dynamic> bibleBlob = await GetBibleBlob();

            dynamic? bibleResult = bibleBlob.FirstOrDefault(x => Convert.ToString(x.Name).ToLower() == name.ToLower().Trim()
            || Convert.ToString(x.Abbreviation).ToLower() == name.ToLower().Trim());

            if (bibleResult is null) return null;
            int count = 0;

            List<List<string>> bookChapters = JsonConvert.DeserializeObject<List<List<string>>>(Convert.ToString(bibleResult.Chapters));

            IEnumerable<dynamic> chaptersDropdown = bookChapters.Select(x => new { Id = count += 1 });

            return new()
            {
                ChapterDropdown = chaptersDropdown,
                BookName = bibleResult.Name,
                Chapters = bookChapters
            };
        }

        public async static Task<VersesResponse> GetAllVersesInAChapterOFTheBible(GetBibleVerseRequest request)
        {
            IEnumerable<dynamic> bibleBlob = await GetBibleBlob();

            dynamic? result = bibleBlob.FirstOrDefault(x => Convert.ToString(x.Name).ToLower() == request.BookNameInFull.ToLower().Trim());
            int count = 1;

            IQueryable<dynamic> chapters = result.Chapters;

            IEnumerable<string> verses = chapters.ElementAt(request.ChapterNumber - first);

            IEnumerable<dynamic> versesDropdown = verses.Select(x => new
            {
                Id = count += 1
            });

            return new()
            {
                VerseDropDown = versesDropdown,
                BookName = result.Name,
                Verses = verses,
            };
        }

        static IEnumerable<dynamic> bibleBlob;

        public async Task<IEnumerable<BibleVerseResponse>> SearchBible()
        {
            string query = "exodus";

            query = string.IsNullOrEmpty(query) ? throw new InvalidOperationException("Invalid query") : query.ToLower().Trim();

            bibleBlob = await GetBibleBlob();

            if (!bibleBlob.Any()) return Enumerable.Empty<BibleVerseResponse>();

            return Enumerable.Empty<BibleVerseResponse>();
        }


        public static IEnumerable<BibleVerseResponse> SearchBibleViaParrallelForEach()
        {
            string query = "exodus";

            List<BibleVerseResponse> result = new();

            int bibleIndex;
            int ChapIndex;
            int VerseIndex;

            Stopwatch sw = new();
            try
            {
                Parallel.ForEach(bibleBlob, (blob, state, index) =>
                            {
                                BlobResponse deserializedBlob = JsonConvert.DeserializeObject<BlobResponse>(Convert.ToString(blob));

                                Parallel.ForEach(deserializedBlob.Chapters, (chapter, state, chapterIndex) =>
                                {
                                    Parallel.ForEach(chapter, (verse, state, verseIndex) =>
                                    {
                                        if (verse.ToLower().Contains(query))
                                        {
                                            result.Add(new()
                                            {
                                                BookName = deserializedBlob.Name,
                                                BookNumber = (int)index + first,
                                                ChapterNumber = (int)chapterIndex + first,
                                                VerseNumber = (int)verseIndex + first,
                                                VerseContent = verse
                                            });
                                        }
                                    });
                                });

                            });
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"ParallelForEach: {sw.ElapsedMilliseconds}");
            }



            return result;
        }


        public static IEnumerable<BibleVerseResponse> SearchBibleViaForEach()
        {
            string query = "exodus";

            List<BibleVerseResponse> result = new();

            int bibleIndex = 0;
            int ChapIndex = 0;
            int VerseIndex = 0;


            Stopwatch sw = new();
            try
            {
                foreach (var blob in bibleBlob)
                {
                    bibleIndex += 1;
                    BlobResponse deserializedBlob = JsonConvert.DeserializeObject<BlobResponse>(Convert.ToString(blob));

                    foreach (var chap in deserializedBlob.Chapters)
                    {
                        ChapIndex += 1;
                        foreach (var verse in chap)
                        {
                            VerseIndex += 1;
                            if (verse.ToLower().Contains(query))
                            {
                                result.Add(new()
                                {
                                    BookName = deserializedBlob.Name,
                                    BookNumber = (int)bibleIndex + first,
                                    ChapterNumber = (int)ChapIndex + first,
                                    VerseNumber = (int)VerseIndex + first,
                                    VerseContent = verse
                                });
                            }
                        }
                    }
                }

            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"ForEach: {sw.ElapsedMilliseconds}");
            }


            return result;
        }


        public static IEnumerable<BibleVerseResponse> SearchBibleViaFor()
        {
            string query = "exodus";

            List<BibleVerseResponse> result = new();

            int bibleIndex;
            int ChapIndex;
            int VerseIndex;

            Stopwatch sw = new();
            try
            {
                for (bibleIndex = 0; bibleIndex < bibleBlob.Count(); bibleIndex++)
                {
                    BlobResponse deserializedBlob = JsonConvert.DeserializeObject<BlobResponse>(Convert.ToString(bibleBlob.ElementAt(bibleIndex)));

                    for (ChapIndex = 0; ChapIndex < deserializedBlob.Chapters.Count(); ChapIndex++)
                    {
                        var chapter = deserializedBlob.Chapters[ChapIndex];

                        for (VerseIndex = 0; VerseIndex < chapter.Count; ChapIndex++)
                        {

                            if (chapter[VerseIndex].ToLower().Contains(query))
                            {
                                result.Add(new()
                                {
                                    BookName = deserializedBlob.Name,
                                    BookNumber = (int)bibleIndex + first,
                                    ChapterNumber = (int)ChapIndex + first,
                                    VerseNumber = (int)VerseIndex + first,
                                    VerseContent = chapter[VerseIndex]
                                });
                            }
                        }
                    }
                }
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"For: {sw.Elapsed}");
            }


            return result;
        }


        public void JustFotTest()
        {

        }


        public record VersesResponse
        {
            public string BookName { get; set; }
            public IEnumerable<string> Verses { get; set; }
            public IEnumerable<dynamic> VerseDropDown { get; set; }
        }
        public record ChaptersResponse
        {
            public string BookName { get; set; }
            public List<List<string>> Chapters { get; set; }
            public IEnumerable<dynamic> ChapterDropdown { get; set; }
        }




        //API

        private static RestClient? _client;

        private static RestClient GetClient()
        {
            _client = null ?? new RestClient("https://gist.githubusercontent.com/king-Alex-d-great/b32f98847970708f4fbba9c94cd9a3a1/raw/97459a7dc59eaeff42c7f5d22cf1553208430e9f/");
            return _client;
        }

        public static async Task<List<dynamic>> GetBibleBlob()
        {
            RestClient client = GetClient();
            RestRequest request = new("kjv.json");

            RestResponse response = await client.ExecuteGetAsync(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException("Error: API call failed\nTip: Check that you are connected to the internet");
            }

            return JsonConvert.DeserializeObject<List<dynamic>>(response.Content);
        }

        public static string Test(string val)
        {
            letter = string.IsNullOrEmpty(letter) ? val : letter;
            return letter;
        }
    }



}
