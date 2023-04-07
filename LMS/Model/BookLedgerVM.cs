using LMS.Data;

namespace LMS.Model
{
    public class BookLedgerVM : BaseVM
    {
        public string ID { get; set; }
        public Guid BookID { get; set; }
        //public virtual Book Book { get; set; }

        public Guid Student_FacultyID { get; set; }
        //public virtual StudentFaculty Student_Faculty { get; set; }


        public DateTime DateIssued { get; set; }
        public DateTime DateToBeReturn { get; set; }

        public bool isReturn { get; set; } = false;
        public DateTime? ReturnDate { get; set; }
    }
}
