namespace LMS.Model
{
    public class BankBookIssueVM : BaseVM
    {
        public string StudentID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }

    }
}
