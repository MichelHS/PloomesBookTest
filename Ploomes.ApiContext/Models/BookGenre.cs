using System.Collections.Generic;

namespace Ploomes.ApiContext.Models
{
    public partial class BookGenre
    {
        public BookGenre()
        {
            Book = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Genre { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
