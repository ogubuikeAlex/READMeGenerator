// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using BibleIndexer.Models.DTOs.Response;
using BibleIndexer.Services.Implementations;

Console.WriteLine("Hello, World!");

//await BibleService.GetBibleVerse(new() { BookNameInFull = "geNEsis", ChapterNumber = 43, VerseNumber = 18});
/*IEnumerable<BibleVerseResponse> result = await BibleService.SearchBible("mary");
result.TryGetNonEnumeratedCount(out int count);

foreach(var item in result)
{
    Console.WriteLine($"{item.BookName} {item.ChapterNumber} {item.VerseNumber}");
}*/


//var resultE = await AZA.GetChaptersInABookOfTheBible("Titus");
//Console.WriteLine(resultE.BookName);

/*foreach (var item in resultE.Chapters)
{
    Console.WriteLine(item.FirstOrDefault());
}*/

/*foreach(var iteem in resultE.ChapterDropdown)
{
    Console.WriteLine(iteem.Id);
}*/

//Console.WriteLine(await BibleService.GenerateRandomBibleVerse());
/*Console.WriteLine(AZA.Test("Test One"));
AZA.Test("Test Two");
Console.WriteLine(AZA.letter);*/
/*var summary = BenchmarkRunner.Run(typeof(AZA));

Console.WriteLine(summary.Table);*/
/*await AZA.GetBlob();*/
//AZA.SearchBibleViaFor();
//AZA.SearchBibleViaForEach();

await BibleService.SearchBible("exodus");



