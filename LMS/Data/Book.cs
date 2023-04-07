using System.ComponentModel.DataAnnotations;

namespace LMS.Data
{
    public class Book
    {
        [Key]
        public Guid ID { get; set; }
        public string BookTitle { get; set; }
        public string Barcode { get; set; }
        public string BatchFor { get; set; }
        public int Quantity { get; set; }
        public int Edition { get; set; }
        public int YearofPublishing { get; set; }
        public int Pages { get; set; }
        public bool BookBank { get; set; } = false;
        public bool Deactive { get; set; } = false;
        public bool EBook { get; set; } = false;
        public string DownloadLink { get; set; }
        public Guid? PublisherID { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public Guid? AuthorID { get; set; }
        public virtual Author? Author { get; set;}
        public Guid? SubjectID { get; set; }
        public virtual Subject? Subject { get; set;}
        public Guid? LanguageID { get; set; }
        public virtual Language? Language { get; set;}
        public Guid? BookTypeID { get; set; }
        public virtual BookType? BookType { get; set; }
    }
}
