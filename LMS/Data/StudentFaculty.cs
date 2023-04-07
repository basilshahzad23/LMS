using System.ComponentModel.DataAnnotations;

namespace LMS.Data
{
    public class StudentFaculty
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Identification { get; set; }
        public string CNIC { get; set; }
        public string UserType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
        public bool Deactive { get; set; } = false;
        public string Email { get; set; }
        public string ContactNumber { get; set; }   
        public string OtherContactNo { get; set; }
        public string HomeAddress { get; set; }
        public string BatchNumber { get; set; }
    }
}
