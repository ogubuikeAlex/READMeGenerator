using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleIndexer.Models.DTOs.Request
{
    internal class BibleRequest
    {
    }

    public record GetBibleVerseRequest
    {
        public int VerseNumber { get; set; }
        public string BookNameInFull { get; set; }
        public int ChapterNumber { get; set; }
    }
}
