using System.ComponentModel.DataAnnotations;

namespace LMS.Data
{
    public class Language
    {
        [Key]
        public Guid ID { get; set; }
        public string LanguageName { get; set; }
    }
}
