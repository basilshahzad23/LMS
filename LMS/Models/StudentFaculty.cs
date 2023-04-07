using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class StudentFaculty
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string FatherName { get; set; } = null!;
        public string Identification { get; set; } = null!;
        public string Cnic { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Email { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string OtherContactNo { get; set; } = null!;
        public string HomeAddress { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
        public bool? Deactive { get; set; }
    }
}
