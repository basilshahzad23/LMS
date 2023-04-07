using LMS.Data;
using LMS.Model;
using System.Threading.Tasks;

namespace LMS.Repository
{
    public interface IAuthorRepository
    {
        Task<List<AuthorVM>> GetAllAuthorsAsync();
        Task<List<AuthorVM>> GetAllAuthorsPagedAsync(int page, int pageSize);
        Task<AuthorVM> DeleteAuthorAsync(AuthorVM Author);
        Task<AuthorVM> EditAuthorAsync(AuthorVM Author);
        Task<AuthorVM> AddAuthorAsync(AuthorVM Author);
        Task<AuthorVM> GetAuthorByIDAsync(Guid id);
    }
    public interface IBookTypeRepository
    {
        Task<List<BookTypeVM>> GetAllBookTypeAsync();
        Task<List<BookTypeVM>> GetAllBookTypePagedAsync(int page, int pageSize);
        Task<BookTypeVM> DeleteBookTypeAsync(BookTypeVM BookType);
        Task<BookTypeVM> EditBookTypeAsync(BookTypeVM BookType);
        Task<BookTypeVM> AddBookTypeAsync(BookTypeVM BookType);
        Task<BookTypeVM> GetBookTypeByIDAsync(Guid id);
    }
    public interface IBookRepository
    {
        Task<List<BookVM>> GetAllBookAsync();
        Task<List<BookVM>> GetAllBookPagedAsync(int page, int pageSize);
        Task<BookVM> DeleteBookAsync(BookVM Book);
        Task<BookVM> EditBookAsync(BookVM Book);
        Task<BookVM> AddBookAsync(BookVM Book);
        Task<BookVM> GetBookByIDAsync(Guid id);
        Task<List<BookVM>> GetBookByTitleAsync(string title);
    }
    public interface IBookLedgerRepository
    {
        Task<List<BookLedgerVM>> GetAllBookLedgerAsync();
        Task<List<BookLedgerVM>> GetAllBookLedgerPagedAsync(int page, int pageSize);
        Task<BookLedgerVM> DeleteBookLedgerAsync(BookLedgerVM BookLedger);
        Task<BookLedgerVM> EditBookLedgerAsync(BookLedgerVM BookLedger);
        Task<BookLedgerVM> GetBookLedgerIssuedAsync(Guid StudentFacultyID, Guid Book_ID);
        Task<BookLedgerVM> IssueBookAsync(BookLedgerVM BookLedger);
        Task<BookLedgerVM> GetBookLedgerByIDAsync(Guid id);
        Task<BankBookIssueVM> IssueBookBankIssueAsync (BankBookIssueVM bankBookIssue);
        Task<BookLedgerVM> ReturnBookAsync(BookLedgerVM BookLedger);
    }
    public interface ILanguageRepository
    {
        Task<List<LanguageVM>> GetAllLanguageAsync();
        Task<List<LanguageVM>> GetAllLanguagePagedAsync(int page, int pageSize);
        Task<LanguageVM> DeleteLanguageAsync(LanguageVM Language);
        Task<LanguageVM> EditLanguageAsync(LanguageVM Language);
        Task<LanguageVM> AddLanguageAsync(LanguageVM Language);
        Task<LanguageVM> GetLanguageByIDAsync(Guid id);
    }
    public interface IPublisherRepository
    {
        Task<List<PublisherVM>> GetAllPublisherAsync();
        Task<List<PublisherVM>> GetAllPublisherPagedAsync(int page, int pageSize);
        Task<PublisherVM> DeletePublisherAsync(PublisherVM Publisher);
        Task<PublisherVM> EditPublisherAsync(PublisherVM Publisher);
        Task<PublisherVM> AddPublisherAsync(PublisherVM Publisher);
        Task<PublisherVM> GetPublisherByIDAsync(Guid id);
    }
    public interface ISubjectRepository
    {
        Task<List<SubjectVM>> GetAllSubjectAsync();
        Task<List<SubjectVM>> GetAllSubjectPagedAsync(int page, int pageSize);
        Task<SubjectVM> DeleteSubjectAsync(SubjectVM Subject);
        Task<SubjectVM> EditSubjectAsync(SubjectVM Subject);
        Task<SubjectVM> AddSubjectAsync(SubjectVM Subject);
        Task<SubjectVM> GetSubjectByIDAsync(Guid id);
    }
    public interface IStudentFacultyRepository
    {
        Task<List<StudentFacultyVM>> GetAllStudent_FacultyAsync();
        Task<List<StudentFacultyVM>> GetAllStudent_FacultyPagedAsync(int page, int pageSize);
        Task<StudentFacultyVM> DeleteStudent_FacultyAsync(StudentFacultyVM Student);
        Task<StudentFacultyVM> EditStudent_FacultyAsync(StudentFacultyVM Student);
        Task<StudentFacultyVM> AddStudent_FacultyAsync(StudentFacultyVM Student);
        Task<StudentFacultyVM> GetStudent_FacultyByIDAsync(Guid id);
    }
}
