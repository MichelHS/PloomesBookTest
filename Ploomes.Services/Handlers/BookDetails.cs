using System;

namespace Ploomes.Services.Handlers
{
    public class BookDetails
    {
        public int Id { get; set; }
        public int? BookGenreId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime? StartedReadingDate { get; set; }
        public DateTime? FinishedReadingDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? StoppedPage { get; set; }
    }
}
