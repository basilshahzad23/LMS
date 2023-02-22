using System.ComponentModel.DataAnnotations;

namespace LMS.Data
{
    public class Author
    {
        [Key]
        public Guid ID { get; set; }
        public string AuthorName { get; set; }
    }
}
