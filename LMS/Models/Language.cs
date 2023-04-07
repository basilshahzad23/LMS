using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class Language
    {
        public Language()
        {
            Books = new HashSet<Book>();
        }

        public Guid Id { get; set; }
        public string LanguageName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
