using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class BookLedger
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid StudentFacultyId { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DateToBeReturn { get; set; }
        public bool IsReturn { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
