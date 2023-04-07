﻿using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class BookType
    {
        public BookType()
        {
            Books = new HashSet<Book>();
        }

        public Guid Id { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
