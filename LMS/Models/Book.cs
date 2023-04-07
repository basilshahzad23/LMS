using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class Book
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public int Quantity { get; set; }
        public int Edition { get; set; }
        public int YearofPublishing { get; set; }
        public int Pages { get; set; }
        public Guid? PublisherId { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? LanguageId { get; set; }
        public Guid? BookTypeId { get; set; }
        public string BatchFor { get; set; } = null!;
        public bool? BookBank { get; set; }
        public bool? Deactive { get; set; }
        public string DownloadLink { get; set; } = null!;
        public bool Ebook { get; set; }

        public virtual Author? Author { get; set; }
        public virtual BookType? BookType { get; set; }
        public virtual Language? Language { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
