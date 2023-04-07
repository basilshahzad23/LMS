using System.ComponentModel.DataAnnotations;

namespace LMS.Data
{
    public class Publisher
    {
        [Key]
        public Guid ID { get; set; }
        public string PublisherName { get; set; }
    }
}
