
# Bible Indexer

A comprehensive library for querying bible content and getting cascading dropdowns for loading books of the bible, chapters and associated verses.


## Author

- [King Alex](https://github.com/king-Alex-d-great)


## Tech Stack

**C#, .Net6.0, .NetStandard2.1**, 



## Doc Reference

#### Get chapters in the Book Of a bible

```C#
   await BibleService.GetChaptersInABookOfTheBible(bookName);   
```

| Parameter  | Type     | Description                         |
| :--------  | :------- | :-------------------------          |
| `bookName` | `string` | **Required**. The name of the book of the bible e.g genesis |


#### Generate a random bible verse

```C#   
   await BibleService.GenerateRandomBibleVerse();
```

#### Get all books of the bible

```C#   
   await BibleService.GetAllBooksOfTheBible();
```

#### Get all verses and verses dropdown in a chapter of the bible

```C#   
   await BibleService.GetAllVersesInAChapterOFTheBible();
```

#### Get a book of the bible

```C#   
   await BibleService.GetBookOfTheBible();
```

#### Get a bible verse

```C#   
   await BibleService.GetBibleVerse();
```

#### Search the bible

```C#   
   await BibleService.SearchBible();
```




## Roadmap


- Robust ReadMe

- Additional features


