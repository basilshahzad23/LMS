using LMS.Data;

namespace LMS.Model
{
    public class BookVM : BaseVM
    {
        public string ID { get; set; }
        public string BookTitle { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public int Edition { get; set; }
        public int YearofPublishing { get; set; }
        public int Pages { get; set; }
        public string? PublisherID { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public string? AuthorID { get; set; }
        public virtual Author? Author { get; set; }
        public string? SubjectID { get; set; }
        public virtual Subject? Subject { get; set; }
        public string? LanguageID { get; set; }
        public virtual Language? Language { get; set; }
        public string? BookTypeID { get; set; }
        public virtual BookType? BookType { get; set; }
        public bool Deactive { get; set; } = false;
        public bool EBook { get; set; } = false;
        public string DownloadLink { get; set; }
        public string BatchFor { get; set; }
        public bool BookBank { get; set; } = false;
    }
}
