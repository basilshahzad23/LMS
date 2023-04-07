using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public Guid Id { get; set; }
        public string PublisherName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
