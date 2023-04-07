using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Books = new HashSet<Book>();
        }

        public Guid Id { get; set; }
        public string SubjectName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
