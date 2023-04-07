using System.ComponentModel.DataAnnotations;

namespace LMS.Data
{
    public class BookType
    {
        [Key]
        public Guid ID { get; set; }
        public string TypeName { get; set; }
    }
}
