using System;
using System.Collections.Generic;
using System.Text;

namespace Ploomes.Services.Handlers
{
    public class BookDetailsCall
    {
        public int? BookGenreId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime? StartedReadingDate { get; set; }
        public DateTime? FinishedReadingDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? StoppedPage { get; set; }
    }
}
