using System.ComponentModel.DataAnnotations;

namespace LMS.Data
{
    public class Subject

    {
        [Key]
        public Guid ID { get; set; }
        public string SubjectName { get; set; }
    }
}
