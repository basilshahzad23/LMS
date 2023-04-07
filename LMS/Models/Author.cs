using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public Guid Id { get; set; }
        public string AuthorName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
