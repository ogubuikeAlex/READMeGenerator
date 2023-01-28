// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using RestSharp;
using BibleIndexer.Models.DTOs.Response;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

Console.WriteLine("Hello, World!");

var result = BenchmarkRunner.Run<Demo>();

[MemoryDiagnoser]
public class Demo
{
    int first = 1;

    // [Benchmark]
    public string GetMethod()
    {
        string outpur = "";

        for (int i = 0; i < 1000; i++)
        {
            outpur += i;
        }

        return outpur;
    }

    // [Benchmark]
    public string GetMethodTwo()
    {
        StringBuilder outpur = new();

        for (int i = 0; i < 1000; i++)
        {
            outpur.Append(i);
        }

        return outpur.ToString();
    }

    static IEnumerable<dynamic> bibleBlob;


    [GlobalSetup]
    public async Task<IEnumerable<BibleVerseResponse>> SearchBible()
    {
        string query = "exodus";

        query = string.IsNullOrEmpty(query) ? throw new InvalidOperationException("Invalid query") : query.ToLower().Trim();

        bibleBlob = await GetBibleBlob();

        if (!bibleBlob.Any()) return Enumerable.Empty<BibleVerseResponse>();

        return Enumerable.Empty<BibleVerseResponse>();
    }

    [Benchmark]
    public void SearchBibleViaParrallelForEach()
    {
        string query = "exodus";
        List<BibleVerseResponse> result = new();

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

        //  return result;
    }

    [Benchmark]
     public void SearchBibleViaForEach()
     {
         string query = "exodus";

         List<BibleVerseResponse> result = new();

         int bibleIndex = 0;
         int ChapIndex = 0;
         int VerseIndex = 0;
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

         //return result;
     }

    [Benchmark]
    public void SearchBibleViaFor()
    {
        string query = "exodus";

        List<BibleVerseResponse> result = new();

        int bibleIndex;
        int ChapIndex;
        int VerseIndex;
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

        // return result;
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

}
