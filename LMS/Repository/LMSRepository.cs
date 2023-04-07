using LMS.AccountRepo;
using LMS.Data;
using LMS.Model;
using LMS.Repository;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Xunit;

namespace LMS.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private LmsDBContext _context;

        public AuthorRepository(LmsDBContext context)
        {
            _context = context;

        }
        public async Task<AuthorVM> AddAuthorAsync(AuthorVM Author)
        {
            try
            {



                var Alreadyrecords = await _context.Authors.Where(x => x.AuthorName == Author.AuthorName).FirstOrDefaultAsync();

                if (Alreadyrecords == null)
                {


                    var record = new Author()
                    {
                        ID = Guid.NewGuid(),
                        AuthorName = Author.AuthorName,

                    };
                    _context.Authors.Add(record);
                    await _context.SaveChangesAsync();
                    Author.ID = record.ID.ToString();
                    Author.StatusCode = "OK";
                    return Author;
                }
                else
                {
                    Author.StatusCode = "error";
                    Author.StatusMsg = "Author with name: " + Author.AuthorName + " already exist , duplication not allowed ";
                    return Author;
                }
            }
            catch (Exception ex)
            {

                Author.StatusCode = "error";
                Author.StatusMsg = ex.Message;
                return Author;
            }
        }

        public async Task<AuthorVM> DeleteAuthorAsync(AuthorVM Author)
        {
            try
            {
                var eRecord = await GetAuthorByIDAsync_d(new Guid(Author.ID));

                if (eRecord != null)
                {

                    _context.Authors.Remove(eRecord);
                    await _context.SaveChangesAsync();

                }
                Author.StatusCode = "OK";
                return Author;
            }
            catch (Exception ex)
            {

                Author.StatusCode = "error";
                Author.StatusMsg = ex.Message;
                return Author;
            }
        }

        public async Task<AuthorVM> EditAuthorAsync(AuthorVM Author)
        {
            try
            {
                var eRecord = await GetAuthorByIDAsync_d(new Guid(Author.ID));
                eRecord.AuthorName = Author.AuthorName;
                _context.Authors.Update(eRecord);
                await _context.SaveChangesAsync();
                Author.StatusCode = "OK";
                return Author;
            }
            catch (Exception ex)
            {
                Author.StatusCode = "error";
                Author.StatusMsg = ex.Message;
                return Author;
            }
        }

        public async Task<List<AuthorVM>> GetAllAuthorsAsync()
        {
            var records = await _context.Authors.Select(x => new AuthorVM()
            {
                ID = x.ID.ToString().ToLower(),
                AuthorName = x.AuthorName,

            }).OrderBy(o => o.AuthorName).ToListAsync();
            return records;
        }

        public async Task<List<AuthorVM>> GetAllAuthorsPagedAsync(int page, int pageSize)
        {
            var records = await _context.Authors.Select(x => new AuthorVM()
            {
                ID = x.ID.ToString().ToLower(),
                AuthorName = x.AuthorName,

            })
                .OrderBy(o => o.AuthorName)
                .Skip(((int)page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            return records;
        }
        public async Task<AuthorVM> GetAuthorByIDAsync(Guid id)
        {

            AuthorVM _Author = new AuthorVM();
            try
            {
                var record = await _context.Authors.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (record != null)
                {
                    _Author.ID = record.ID.ToString();
                    _Author.AuthorName = record.AuthorName;
                    return _Author;
                }
                else
                {
                    _Author.StatusCode = "OK";
                    _Author.StatusMsg = "No Such Author Found";
                    return _Author;

                }
            }
            catch (Exception ex)
            {
                _Author.StatusCode = "error";
                _Author.StatusMsg = ex.Message;
                return _Author;
            }

        }

        private async Task<Author> GetAuthorByIDAsync_d(Guid id)
        {
            var records = await _context.Authors.Where(x => x.ID == id).FirstOrDefaultAsync();

            return records;

        }
    }

    public class BookLedgerRepository : IBookLedgerRepository
    {
        private LmsDBContext _context;

        public BookLedgerRepository(LmsDBContext context)
        {
            _context = context;

        }
        public async Task<BookLedgerVM> IssueBookAsync(BookLedgerVM BookLedger)
        {
            try
            {
                var isdeactive = await _context.Books.Where(x => x.ID == BookLedger.BookID).FirstOrDefaultAsync();
                //var isEBook = await _context.Books.Where(x => x.ID == BookLedger.BookID).ToListAsync();
                var studentdeactive = await _context.Student_Faculty.Where(x => x.ID == BookLedger.Student_FacultyID).FirstOrDefaultAsync();
                var Alreadyrecords = await _context.BookLedgers.Where(x => x.BookID == BookLedger.Student_FacultyID && x.BookID == BookLedger.BookID && x.isReturn == false).ToListAsync();
                var issuedBookCount = await _context.BookLedgers.CountAsync(x => x.Student_FacultyID == BookLedger.Student_FacultyID && x.isReturn == false);
                

                if (Alreadyrecords.Count > 0)
                {
                    BookLedger.StatusCode = "error";
                    BookLedger.StatusMsg = "BookLedger with name: " + BookLedger.Student_FacultyID + " already exist , duplication not allowed ";
                    return BookLedger;
                }

                if (issuedBookCount > 2)
                {
                    BookLedger.StatusCode = "error";
                    BookLedger.StatusMsg = "Student has already issued 3 books, cannot issue more books";
                    return BookLedger;
                }

                if (isdeactive.Deactive == true) 
                {
                    BookLedger.StatusCode = "error";
                    BookLedger.StatusMsg = "Cannot issue "+ isdeactive.BookTitle +" is Deactivated";
                    return BookLedger;
                }

                //isdeactive is also used in EBook 
                if (isdeactive.EBook == true)
                {
                    BookLedger.StatusCode = "error";
                    BookLedger.StatusMsg = "Cannot issue " + isdeactive.BookTitle + " is EBook, Download EBook from here " + isdeactive.DownloadLink;
                    return BookLedger;
                }

                if (studentdeactive.Deactive == true)
                {
                    BookLedger.StatusCode = "error";
                    BookLedger.StatusMsg = "Cannot issue " + studentdeactive.Name + " is Deactivated";
                    return BookLedger;
                }
                
               


                else
                {
                    var record = new BookLedger()
                    {
                        ID = Guid.NewGuid(),
                        BookID = BookLedger.BookID,
                        Student_FacultyID = BookLedger.Student_FacultyID,
                        DateIssued = BookLedger.DateIssued,
                        DateToBeReturn = BookLedger.DateToBeReturn, 
                    };
                    _context.BookLedgers.Add(record);
                    await _context.SaveChangesAsync();
                    BookLedger.ID = record.ID.ToString();
                    BookLedger.StatusCode = "OK";
                    return BookLedger;

                }
            }
            catch (Exception ex)
            {

                BookLedger.StatusCode = "error";
                BookLedger.StatusMsg = ex.Message;
                return BookLedger;
            }
        }
        public async Task<BankBookIssueVM> IssueBookBankIssueAsync(BankBookIssueVM bankBookIssue)
        {
            try
            {
                var _student = await _context.Student_Faculty.Where(x => x.ID == new Guid(bankBookIssue.StudentID)).FirstOrDefaultAsync();
                var _SameBooks = await _context.Books.Where(x => x.BatchFor == _student.BatchNumber && x.BookBank == true).ToListAsync();
                var errors = new List<string>();

                foreach (var b in _SameBooks)
                {
                    var Alreadyrecords = await _context.BookLedgers.Where(x => x.BookID == b.ID && x.isReturn == false && x.Student_FacultyID == new Guid(bankBookIssue.StudentID)).FirstOrDefaultAsync();
                    if (Alreadyrecords != null)
                    {
                        //errors.Add("Book with Name: " + b.BookTitle + " already Issued , Cannot Issue again.");
                        bankBookIssue.StatusCode = "OK";
                        bankBookIssue.StatusMsg = bankBookIssue.StatusMsg + b.BookTitle + " is already issued. ";

                        continue;
                    }
                    
                    {
                        var record = new BookLedger()
                        {
                            ID = Guid.NewGuid(),
                            BookID = b.ID,
                            Student_FacultyID = new Guid(bankBookIssue.StudentID),
                            DateIssued = DateTime.Now,
                            DateToBeReturn = bankBookIssue.TillDate,
                            ReturnDate=null ,
                            isReturn=false
                        };
                        _context.BookLedgers.Add(record);
                        await _context.SaveChangesAsync();
                    }
                    bankBookIssue.StatusCode = "OK";
                    bankBookIssue.StatusMsg = bankBookIssue.StatusMsg + b.BookTitle + " is issued. ";
                    
                }
                return bankBookIssue;
            }
            catch (Exception ex)
            { 
              
                    bankBookIssue.StatusCode = "error";
                    bankBookIssue.StatusMsg = ex.Message;
                    return bankBookIssue;
                
            }
        }
        public async Task<BookLedgerVM> ReturnBookAsync(BookLedgerVM BookLedger)
        {


            try
            {


                var record = new BookLedger()
                {
                    ID = new Guid(BookLedger.ID),
                    BookID = BookLedger.BookID,
                    Student_FacultyID = BookLedger.Student_FacultyID,
                    DateToBeReturn= BookLedger.DateToBeReturn,
                    ReturnDate = BookLedger.ReturnDate,
                    isReturn = true
                };
                _context.BookLedgers.Update(record);
                await _context.SaveChangesAsync();
                BookLedger.isReturn = true;
                BookLedger.StatusCode = "OK";
                return BookLedger;

            }
            catch (Exception ex)
            {
                BookLedger.StatusCode = "error";
                BookLedger.StatusMsg = ex.Message;
                return BookLedger;
            }
        }
        public async Task<BookLedgerVM> DeleteBookLedgerAsync(BookLedgerVM BookLedger)
        {
            try
            {
                var eRecord = await GetBookLedgerByIDAsync_d(new Guid(BookLedger.ID));

                if (eRecord != null)
                {

                    _context.BookLedgers.Remove(eRecord);
                    await _context.SaveChangesAsync();

                }
                BookLedger.StatusCode = "OK";
                return BookLedger;
            }
            catch (Exception ex)
            {

                BookLedger.StatusCode = "error";
                BookLedger.StatusMsg = ex.Message;
                return BookLedger;
            }
        }
        public async Task<BookLedgerVM> EditBookLedgerAsync(BookLedgerVM BookLedger)
        {
            try
            {
                var eRecord = await GetBookLedgerByIDAsync_d(new Guid(BookLedger.ID));
                _context.BookLedgers.Update(eRecord);
                await _context.SaveChangesAsync();
                BookLedger.StatusCode = "OK";
                return BookLedger;
            }
            catch (Exception ex)
            {
                BookLedger.StatusCode = "error";
                BookLedger.StatusMsg = ex.Message;
                return BookLedger;
            }
        }
        public async Task<BookLedgerVM> GetBookLedgerIssuedAsync(Guid StudentFacultyID, Guid Book_ID)
        {
            try
            {
                //var bookList = new List<BookLedgerVM>();
                BookLedgerVM _book = new BookLedgerVM();
                var record = await _context.BookLedgers.FirstOrDefaultAsync(x => x.Student_FacultyID == StudentFacultyID && x.BookID == Book_ID && x.isReturn == false);
                if (record != null)
                {
                    _book.ID = record.ID.ToString();
                    _book.BookID = record.BookID;
                    _book.Student_FacultyID = record.Student_FacultyID;
                    _book.isReturn = record.isReturn;
                    _book.StatusCode = "OK";
                    return _book;
                }
                else
                {
                    _book.StatusCode = "Not Found";
                    _book.StatusMsg = "No records found";
                }
                return _book;
            }
            catch (Exception ex)
            {
                BookLedgerVM _book = new BookLedgerVM();
                _book.StatusCode = "error";
                _book.StatusMsg = ex.Message;
                return _book;
            }
        }
        public async Task<List<BookLedgerVM>> GetAllBookLedgerAsync()
        {
            var records = await _context.BookLedgers.Select(x => new BookLedgerVM()
            {
                ID = x.ID.ToString().ToLower(),

            }).OrderBy(o => o.ID).ToListAsync();
            return records;
        }
        public async Task<List<BookLedgerVM>> GetAllBookLedgerPagedAsync(int page, int pageSize)
        {
            var records = await _context.BookLedgers.Select(x => new BookLedgerVM()
            {
                ID = x.ID.ToString().ToLower(),

            })
                .OrderBy(o => o.ID)
                .Skip(((int)page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            return records;
        }
        public async Task<BookLedgerVM> GetBookLedgerByIDAsync(Guid id)
        {

            BookLedgerVM _BookLedger = new BookLedgerVM();
            try
            {
                var record = await _context.BookLedgers.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (record != null)
                {
                    _BookLedger.ID = record.ID.ToString();
                    return _BookLedger;
                }
                else
                {
                    _BookLedger.StatusCode = "OK";
                    _BookLedger.StatusMsg = "No Such BookLedger Found";
                    return _BookLedger;

                }
            }
            catch (Exception ex)
            {
                _BookLedger.StatusCode = "error";
                _BookLedger.StatusMsg = ex.Message;
                return _BookLedger;
            }
     

        }
        private async Task<BookLedger> GetBookLedgerByIDAsync_d(Guid id)
        {
            var records = await _context.BookLedgers.Where(x => x.ID == id).FirstOrDefaultAsync();

            return records;

        }
    }
    public class BookTypeRepository : IBookTypeRepository
    {
        private LmsDBContext _context;

        public BookTypeRepository(LmsDBContext context)
        {
            _context = context;

        }
        public async Task<BookTypeVM> AddBookTypeAsync(BookTypeVM BookType)
        {
            try
            {



                var Alreadyrecords = await _context.BookTypes.Where(x => x.TypeName == BookType.TypeName).FirstOrDefaultAsync();

                if (Alreadyrecords == null)
                {


                    var record = new BookType()
                    {
                        ID = Guid.NewGuid(),
                        TypeName = BookType.TypeName,

                    };
                    _context.BookTypes.Add(record);
                    await _context.SaveChangesAsync();
                    BookType.TypeName = record.ID.ToString();
                    BookType.StatusCode = "OK";
                    return BookType;
                }
                else
                {
                    BookType.StatusCode = "error";
                    BookType.StatusMsg = "BookType with name: " + BookType.TypeName + " already exist , duplication not allowed ";
                    return BookType;
                }
            }
            catch (Exception ex)
            {

                BookType.StatusCode = "error";
                BookType.StatusMsg = ex.Message;
                return BookType;
            }
        }

        public async Task<BookTypeVM> DeleteBookTypeAsync(BookTypeVM BookType)
        {
            try
            {
                var eRecord = await GetBookTypeByIDAsync_d(new Guid(BookType.ID));

                if (eRecord != null)
                {

                    _context.BookTypes.Remove(eRecord);
                    await _context.SaveChangesAsync();

                }
                BookType.StatusCode = "OK";
                return BookType;
            }
            catch (Exception ex)
            {

                BookType.StatusCode = "error";
                BookType.StatusMsg = ex.Message;
                return BookType;
            }
        }

        public async Task<BookTypeVM> EditBookTypeAsync(BookTypeVM BookType)
        {
            try
            {
                var eRecord = await GetBookTypeByIDAsync_d(new Guid(BookType.ID));
                eRecord.TypeName = BookType.TypeName;
                _context.BookTypes.Update(eRecord);
                await _context.SaveChangesAsync();
                BookType.StatusCode = "OK";
                return BookType;
            }
            catch (Exception ex)
            {
                BookType.StatusCode = "error";
                BookType.StatusMsg = ex.Message;
                return BookType;
            }
        }

        public async Task<List<BookTypeVM>> GetAllBookTypeAsync()
        {
            var records = await _context.BookTypes.Select(x => new BookTypeVM()
            {
                ID = x.ID.ToString().ToLower(),
                TypeName = x.TypeName,

            }).OrderBy(o => o.TypeName).ToListAsync();
            return records;
        }

        public async Task<List<BookTypeVM>> GetAllBookTypePagedAsync(int page, int pageSize)
        {
            var records = await _context.BookTypes.Select(x => new BookTypeVM()
            {
                ID = x.ID.ToString().ToLower(),
                TypeName = x.TypeName,

            })
                .OrderBy(o => o.TypeName)
                .Skip(((int)page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            return records;
        }
        public async Task<BookTypeVM> GetBookTypeByIDAsync(Guid id)
        {

            BookTypeVM _BookType = new BookTypeVM();
            try
            {
                var record = await _context.BookTypes.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (record != null)
                {
                    _BookType.ID = record.ID.ToString();
                    _BookType.TypeName = record.TypeName;
                    return _BookType;
                }
                else
                {
                    _BookType.StatusCode = "OK";
                    _BookType.StatusMsg = "No Such BookType Found";
                    return _BookType;

                }
            }
            catch (Exception ex)
            {
                _BookType.StatusCode = "error";
                _BookType.StatusMsg = ex.Message;
                return _BookType;
            }

        }

        private async Task<BookType> GetBookTypeByIDAsync_d(Guid id)
        {
            var records = await _context.BookTypes.Where(x => x.ID == id).FirstOrDefaultAsync();

            return records;

        }
    }
    public class BookRepository : IBookRepository
    {
        private LmsDBContext _context;

        public BookRepository(LmsDBContext context)
        {
            _context = context;

        }
        public async Task<BookVM> AddBookAsync(BookVM Book)
        {
            try
            {



                var Alreadyrecords = await _context.Books.Where(x => x.BookTitle == Book.BookTitle).FirstOrDefaultAsync();

                if (Alreadyrecords == null)
                {


                    var record = new Book()
                    {
                        ID = Guid.NewGuid(),
                        BookTitle = Book.BookTitle,
                        Barcode = Book.Barcode,
                        Quantity = Book.Quantity,
                        Edition = Book.Edition,
                        YearofPublishing = Book.YearofPublishing,
                        Pages = Book.Pages,
                        BatchFor = Book.BatchFor,
                        Deactive = Book.Deactive,
                        EBook = Book.EBook,
                        DownloadLink = Book.DownloadLink,
                        BookBank = Book.BookBank

                    };
                    _context.Books.Add(record);
                    await _context.SaveChangesAsync();
                    Book.BookTitle = record.ID.ToString();
                    Book.StatusCode = "OK";
                    return Book;
                }
                else
                {
                    Book.StatusCode = "error";
                    Book.StatusMsg = "Book with name: " + Book.BookTitle + " already exist , duplication not allowed ";
                    return Book;
                }
            }
            catch (Exception ex)
            {

                Book.StatusCode = "error";
                Book.StatusMsg = ex.Message;
                return Book;
            }
        }

        public async Task<BookVM> DeleteBookAsync(BookVM Book)
        {
            try
            {
                var eRecord = await GetBookByIDAsync_d(new Guid(Book.ID));

                if (eRecord != null)
                {

                    _context.Books.Remove(eRecord);
                    await _context.SaveChangesAsync();

                }
                Book.StatusCode = "OK";
                return Book;
            }
            catch (Exception ex)
            {

                Book.StatusCode = "error";
                Book.StatusMsg = ex.Message;
                return Book;
            }
        }

        public async Task<BookVM> EditBookAsync(BookVM Book)
        {
            try
            {
                var eRecord = await GetBookByIDAsync_d(new Guid(Book.ID));
                eRecord.BookTitle = Book.BookTitle;
                eRecord.Barcode = Book.Barcode;
                eRecord.Quantity = Book.Quantity;
                eRecord.Edition = Book.Edition;
                eRecord.YearofPublishing = Book.YearofPublishing;
                eRecord.Pages = Book.Pages;
                _context.Books.Update(eRecord);
                await _context.SaveChangesAsync();
                Book.StatusCode = "OK";
                return Book;
            }
            catch (Exception ex)
            {
                Book.StatusCode = "error";
                Book.StatusMsg = ex.Message;
                return Book;
            }
        }
        

        public async Task<List<BookVM>> GetAllBookAsync()
        {
            var records = await _context.Books.Select(x => new BookVM()
            {
                ID = x.ID.ToString().ToLower(),
                BookTitle = x.BookTitle,
                Barcode = x.Barcode,
                Quantity = x.Quantity,
                Edition = x.Edition,
                YearofPublishing = x.YearofPublishing,
                Pages = x.Pages

            }).OrderBy(o => o.BookTitle).ToListAsync();
            return records;
        }

        public async Task<List<BookVM>> GetAllBookPagedAsync(int page, int pageSize)
        {
            var records = await _context.Books.Select(x => new BookVM()
            {
                ID = x.ID.ToString().ToLower(),
                BookTitle = x.BookTitle,
                Barcode = x.Barcode,
                Quantity = x.Quantity,
                Edition = x.Edition,
                YearofPublishing = x.YearofPublishing,
                Pages = x.Pages

            })
                .OrderBy(o => o.BookTitle)
                .Skip(((int)page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            return records;
        }
        public async Task<BookVM> GetBookByIDAsync(Guid id)
        {

            BookVM _Book = new BookVM();
            try
            {
                var record = await _context.Books.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (record != null)
                {
                    _Book.ID = record.ID.ToString();
                    _Book.BookTitle = record.BookTitle;
                    _Book.Barcode = record.Barcode;
                    _Book.Quantity = record.Quantity;
                    _Book.Edition = record.Edition;
                    _Book.YearofPublishing = record.YearofPublishing;
                    _Book.Pages = record.Pages;
                    return _Book;
                }
                else
                {
                    _Book.StatusCode = "OK";
                    _Book.StatusMsg = "No Such BookType Found";
                    return _Book;

                }
            }
            catch (Exception ex)
            {
                _Book.StatusCode = "error";
                _Book.StatusMsg = ex.Message;
                return _Book;
            }

        }

        private async Task<Book> GetBookByIDAsync_d(Guid id)
        {
            var records = await _context.Books.Where(x => x.ID == id).FirstOrDefaultAsync();

            return records;

        }

        public async Task<List<BookVM>> GetBookByTitleAsync(string title)
        {
            List<BookVM> bookList = new List<BookVM>();
            try
            {
                var records = await _context.Books.Where(x => x.BookTitle.Contains(title)).ToListAsync();
                if (records.Count > 0)
                {
                    foreach (var record in records)
                    {
                        BookVM _Book = new BookVM();
                        _Book.ID = record.ID.ToString();
                        _Book.BookTitle = record.BookTitle;
                        _Book.Barcode = record.Barcode;
                        _Book.Quantity = record.Quantity;
                        _Book.Edition = record.Edition;
                        _Book.YearofPublishing = record.YearofPublishing;
                        _Book.Pages = record.Pages;
                        bookList.Add(_Book);
                    }
                    return bookList;
                }
                else
                {
                    BookVM _Book = new BookVM();
                    _Book.StatusCode = "OK";
                    _Book.StatusMsg = "No Such BookType Found";
                    bookList.Add(_Book);
                    return bookList;
                }
            }
            catch (Exception ex)
            {
                BookVM _Book = new BookVM();
                _Book.StatusCode = "error";
                _Book.StatusMsg = ex.Message;
                bookList.Add(_Book);
                return bookList;
            }
        }

    }
    public class LanguageRepository : ILanguageRepository
    {
        private LmsDBContext _context;

        public LanguageRepository(LmsDBContext context)
        {
            _context = context;

        }
        public async Task<LanguageVM> AddLanguageAsync(LanguageVM Language)
        {
            try
            {



                var Alreadyrecords = await _context.Languages.Where(x => x.LanguageName == Language.LanguageName).FirstOrDefaultAsync();

                if (Alreadyrecords == null)
                {


                    var record = new Language()
                    {
                        ID = Guid.NewGuid(),
                        LanguageName = Language.LanguageName,

                    };
                    _context.Languages.Add(record);
                    await _context.SaveChangesAsync();
                    Language.ID = record.ID.ToString();
                    Language.StatusCode = "OK";
                    return Language;
                }
                else
                {
                    Language.StatusCode = "error";
                    Language.StatusMsg = "Language with name: " + Language.LanguageName + " already exist , duplication not allowed ";
                    return Language;
                }
            }
            catch (Exception ex)
            {

                Language.StatusCode = "error";
                Language.StatusMsg = ex.Message;
                return Language;
            }
        }

        public async Task<LanguageVM> DeleteLanguageAsync(LanguageVM Language)
        {
            try
            {
                var eRecord = await GetLanguageByIDAsync_d(new Guid(Language.ID));

                if (eRecord != null)
                {

                    _context.Languages.Remove(eRecord);
                    await _context.SaveChangesAsync();

                }
                Language.StatusCode = "OK";
                return Language;
            }
            catch (Exception ex)
            {

                Language.StatusCode = "error";
                Language.StatusMsg = ex.Message;
                return Language;
            }
        }

        public async Task<LanguageVM> EditLanguageAsync(LanguageVM Language)
        {
            try
            {
                var eRecord = await GetLanguageByIDAsync_d(new Guid(Language.ID));
                eRecord.LanguageName = Language.LanguageName;
                _context.Languages.Update(eRecord);
                await _context.SaveChangesAsync();
                Language.StatusCode = "OK";
                return Language;
            }
            catch (Exception ex)
            {
                Language.StatusCode = "error";
                Language.StatusMsg = ex.Message;
                return Language;
            }
        }

        public async Task<List<LanguageVM>> GetAllLanguageAsync()
        {
            var records = await _context.Languages.Select(x => new LanguageVM()
            {
                ID = x.ID.ToString().ToLower(),
                LanguageName = x.LanguageName,

            }).OrderBy(o => o.LanguageName).ToListAsync();
            return records;
        }

        public async Task<List<LanguageVM>> GetAllLanguagePagedAsync(int page, int pageSize)
        {
            var records = await _context.Languages.Select(x => new LanguageVM()
            {
                ID = x.ID.ToString().ToLower(),
                LanguageName = x.LanguageName,

            })
                .OrderBy(o => o.LanguageName)
                .Skip(((int)page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            return records;
        }
        public async Task<LanguageVM> GetLanguageByIDAsync(Guid id)
        {

            LanguageVM _Language = new LanguageVM();
            try
            {
                var record = await _context.Languages.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (record != null)
                {
                    _Language.ID = record.ID.ToString();
                    _Language.LanguageName = record.LanguageName;
                    return _Language;
                }
                else
                {
                    _Language.StatusCode = "OK";
                    _Language.StatusMsg = "No Such Language Found";
                    return _Language;

                }
            }
            catch (Exception ex)
            {
                _Language.StatusCode = "error";
                _Language.StatusMsg = ex.Message;
                return _Language;
            }

        }

        private async Task<Language> GetLanguageByIDAsync_d(Guid id)
        {
            var records = await _context.Languages.Where(x => x.ID == id).FirstOrDefaultAsync();

            return records;

        }
    }
    public class PublisherRepository : IPublisherRepository
    {
        private LmsDBContext _context;

        public PublisherRepository(LmsDBContext context)
        {
            _context = context;

        }
        public async Task<PublisherVM> AddPublisherAsync(PublisherVM Publisher)
        {
            try
            {



                var Alreadyrecords = await _context.Publishers.Where(x => x.PublisherName == Publisher.PublisherName).FirstOrDefaultAsync();

                if (Alreadyrecords == null)
                {


                    var record = new Publisher()
                    {
                        ID = Guid.NewGuid(),
                        PublisherName = Publisher.PublisherName,

                    };
                    _context.Publishers.Add(record);
                    await _context.SaveChangesAsync();
                    Publisher.ID = record.ID.ToString();
                    Publisher.StatusCode = "OK";
                    return Publisher;
                }
                else
                {
                    Publisher.StatusCode = "error";
                    Publisher.StatusMsg = "Publisher with name: " + Publisher.PublisherName + " already exist , duplication not allowed ";
                    return Publisher;
                }
            }
            catch (Exception ex)
            {

                Publisher.StatusCode = "error";
                Publisher.StatusMsg = ex.Message;
                return Publisher;
            }
        }

        public async Task<PublisherVM> DeletePublisherAsync(PublisherVM Publisher)
        {
            try
            {
                var eRecord = await GetPublisherByIDAsync_d(new Guid(Publisher.ID));

                if (eRecord != null)
                {

                    _context.Publishers.Remove(eRecord);
                    await _context.SaveChangesAsync();

                }
                Publisher.StatusCode = "OK";
                return Publisher;
            }
            catch (Exception ex)
            {

                Publisher.StatusCode = "error";
                Publisher.StatusMsg = ex.Message;
                return Publisher;
            }
        }

        public async Task<PublisherVM> EditPublisherAsync(PublisherVM Publisher)
        {
            try
            {
                var eRecord = await GetPublisherByIDAsync_d(new Guid(Publisher.ID));
                eRecord.PublisherName = Publisher.PublisherName;
                _context.Publishers.Update(eRecord);
                await _context.SaveChangesAsync();
                Publisher.StatusCode = "OK";
                return Publisher;
            }
            catch (Exception ex)
            {
                Publisher.StatusCode = "error";
                Publisher.StatusMsg = ex.Message;
                return Publisher;
            }
        }

        public async Task<List<PublisherVM>> GetAllPublisherAsync()
        {
            var records = await _context.Publishers.Select(x => new PublisherVM()
            {
                ID = x.ID.ToString().ToLower(),
                PublisherName = x.PublisherName,

            }).OrderBy(o => o.PublisherName).ToListAsync();
            return records;
        }

        public async Task<List<PublisherVM>> GetAllPublisherPagedAsync(int page, int pageSize)
        {
            var records = await _context.Publishers.Select(x => new PublisherVM()
            {
                ID = x.ID.ToString().ToLower(),
                PublisherName = x.PublisherName,

            })
                .OrderBy(o => o.PublisherName)
                .Skip(((int)page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            return records;
        }
        public async Task<PublisherVM> GetPublisherByIDAsync(Guid id)
        {

            PublisherVM _Publisher = new PublisherVM();
            try
            {
                var record = await _context.Publishers.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (record != null)
                {
                    _Publisher.ID = record.ID.ToString();
                    _Publisher.PublisherName = record.PublisherName;
                    return _Publisher;
                }
                else
                {
                    _Publisher.StatusCode = "OK";
                    _Publisher.StatusMsg = "No Such Language Found";
                    return _Publisher;

                }
            }
            catch (Exception ex)
            {
                _Publisher.StatusCode = "error";
                _Publisher.StatusMsg = ex.Message;
                return _Publisher;
            }

        }

        private async Task<Publisher> GetPublisherByIDAsync_d(Guid id)
        {
            var records = await _context.Publishers.Where(x => x.ID == id).FirstOrDefaultAsync();

            return records;

        }
    }
    public class SubjectRepository : ISubjectRepository
    {
        private LmsDBContext _context;

        public SubjectRepository(LmsDBContext context)
        {
            _context = context;

        }
        public async Task<SubjectVM> AddSubjectAsync(SubjectVM Subject)
        {
            try
            {



                var Alreadyrecords = await _context.Subjects.Where(x => x.SubjectName == Subject.SubjectName).FirstOrDefaultAsync();

                if (Alreadyrecords == null)
                {


                    var record = new Subject()
                    {
                        ID = Guid.NewGuid(),
                        SubjectName = Subject.SubjectName,

                    };
                    _context.Subjects.Add(record);
                    await _context.SaveChangesAsync();
                    Subject.ID = record.ID.ToString();
                    Subject.StatusCode = "OK";
                    return Subject;
                }
                else
                {
                    Subject.StatusCode = "error";
                    Subject.StatusMsg = "Subject with name: " + Subject.SubjectName + " already exist , duplication not allowed ";
                    return Subject;
                }
            }
            catch (Exception ex)
            {

                Subject.StatusCode = "error";
                Subject.StatusMsg = ex.Message;
                return Subject;
            }
        }

        public async Task<SubjectVM> DeleteSubjectAsync(SubjectVM Subject)
        {
            try
            {
                var eRecord = await GetSubjectByIDAsync_d(new Guid(Subject.ID));

                if (eRecord != null)
                {

                    _context.Subjects.Remove(eRecord);
                    await _context.SaveChangesAsync();

                }
                Subject.StatusCode = "OK";
                return Subject;
            }
            catch (Exception ex)
            {

                Subject.StatusCode = "error";
                Subject.StatusMsg = ex.Message;
                return Subject;
            }
        }

        public async Task<SubjectVM> EditSubjectAsync(SubjectVM Subject)
        {
            try
            {
                var eRecord = await GetSubjectByIDAsync_d(new Guid(Subject.ID));
                eRecord.SubjectName = Subject.SubjectName;
                _context.Subjects.Update(eRecord);
                await _context.SaveChangesAsync();
                Subject.StatusCode = "OK";
                return Subject;
            }
            catch (Exception ex)
            {
                Subject.StatusCode = "error";
                Subject.StatusMsg = ex.Message;
                return Subject;
            }
        }

        public async Task<List<SubjectVM>> GetAllSubjectAsync()
        {
            var records = await _context.Subjects.Select(x => new SubjectVM()
            {
                ID = x.ID.ToString().ToLower(),
                SubjectName = x.SubjectName,

            }).OrderBy(o => o.SubjectName).ToListAsync();
            return records;
        }

        public async Task<List<SubjectVM>> GetAllSubjectPagedAsync(int page, int pageSize)
        {
            var records = await _context.Subjects.Select(x => new SubjectVM()
            {
                ID = x.ID.ToString().ToLower(),
                SubjectName = x.SubjectName,

            })
                .OrderBy(o => o.SubjectName)
                .Skip(((int)page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            return records;
        }
        public async Task<SubjectVM> GetSubjectByIDAsync(Guid id)
        {

            SubjectVM _Subject = new SubjectVM();
            try
            {
                var record = await _context.Subjects.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (record != null)
                {
                    _Subject.ID = record.ID.ToString();
                    _Subject.SubjectName = record.SubjectName;
                    return _Subject;
                }
                else
                {
                    _Subject.StatusCode = "OK";
                    _Subject.StatusMsg = "No Such Subject Found";
                    return _Subject;

                }
            }
            catch (Exception ex)
            {
                _Subject.StatusCode = "error";
                _Subject.StatusMsg = ex.Message;
                return _Subject;
            }

        }

        private async Task<Subject> GetSubjectByIDAsync_d(Guid id)
        {
            var records = await _context.Subjects.Where(x => x.ID == id).FirstOrDefaultAsync();

            return records;

        }
    }
    public class Student_FacultyRepository : IStudentFacultyRepository
    {
        private LmsDBContext _context;

        public Student_FacultyRepository(LmsDBContext context)
        {
            _context = context;

        }
        public async Task<StudentFacultyVM> AddStudent_FacultyAsync(StudentFacultyVM Student)
        {
            try
            {



                var Alreadyrecords = await _context.Student_Faculty.Where(x => x.Identification == Student.Identification || x.Email == Student.Email).FirstOrDefaultAsync();

                if (Alreadyrecords == null)
                {


                    var record = new StudentFaculty()
                    {
                        ID = Guid.NewGuid(),
                        Name = Student.Name,
                        FatherName = Student.FatherName,
                        Identification = Student.Identification,
                        CNIC = Student.CNIC,
                        UserType = Student.UserType,
                        Deactive = Student.Deactive,
                        StartDate = Student.StartDate,
                        EndDate = Student.EndDate,
                        Email = Student.Email,
                        ContactNumber = Student.ContactNumber,
                        OtherContactNo = Student.OtherContactNo,
                        HomeAddress = Student.HomeAddress,
                        BatchNumber = Student.BatchNumber
                    };
                    _context.Student_Faculty.Add(record);
                    await _context.SaveChangesAsync();
                    Student.ID = record.ID.ToString();
                    Student.StatusCode = "OK";
                    return Student;
                }
                else
                {
                    Student.StatusCode = "error";
                    Student.StatusMsg = "Student with Identification: " + Student.Identification + " already exist, Student with Email" + Student.Email +  "already exist, duplication not allowed ";
                    return Student;
                }
            }
            catch (Exception ex)
            {

                Student.StatusCode = "error";
                Student.StatusMsg = ex.Message;
                return Student;
            }
        }

        public async Task<StudentFacultyVM> DeleteStudent_FacultyAsync(StudentFacultyVM Student)
        {
            try
            {
                var eRecord = await GetStudent_FacultyByIDAsync_d(new Guid(Student.ID));

                if (eRecord != null)
                {

                    _context.Student_Faculty.Remove(eRecord);
                    await _context.SaveChangesAsync();

                }
                Student.StatusCode = "OK";
                return Student;
            }
            catch (Exception ex)
            {

                Student.StatusCode = "error";
                Student.StatusMsg = ex.Message;
                return Student;
            }
        }

        public async Task<StudentFacultyVM> EditStudent_FacultyAsync(StudentFacultyVM Student)
        {
            try
            {
                var eRecord = await GetStudent_FacultyByIDAsync_d(new Guid(Student.ID));
                eRecord.Name = Student.Name;
                eRecord.FatherName = Student.FatherName;
                eRecord.Identification = Student.Identification;
                eRecord.CNIC = Student.CNIC;
                eRecord.UserType = Student.UserType;
                eRecord.StartDate = Student.StartDate;
                eRecord.EndDate = Student.EndDate;
                eRecord.Deactive = Student.Deactive;
                eRecord.Email = Student.Email;
                eRecord.ContactNumber = Student.ContactNumber;
                eRecord.OtherContactNo = Student.OtherContactNo;
                eRecord.HomeAddress = Student.HomeAddress;
                eRecord.BatchNumber = Student.BatchNumber;
                _context.Student_Faculty.Update(eRecord);
                await _context.SaveChangesAsync();
                Student.StatusCode = "OK";
                return Student;
            }
            catch (Exception ex)
            {
                Student.StatusCode = "error";
                Student.StatusMsg = ex.Message;
                return Student;
            }
        }

        public async Task<List<StudentFacultyVM>> GetAllStudent_FacultyAsync()
        {
            var records = await _context.Student_Faculty.Select(x => new StudentFacultyVM()
            {
                ID = x.ID.ToString().ToLower(),
                Name = x.Name,
                FatherName = x.FatherName,
                Identification = x.Identification,
                CNIC = x.CNIC,
                UserType = x.UserType,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Email = x.Email,
                ContactNumber = x.ContactNumber,
                HomeAddress = x.HomeAddress,
                OtherContactNo = x.OtherContactNo,
                BatchNumber = x.BatchNumber,
            }).OrderBy(o => o.Name).ToListAsync();
            return records;
        }

        public async Task<List<StudentFacultyVM>> GetAllStudent_FacultyPagedAsync(int page, int pageSize)
        {
            var records = await _context.Student_Faculty.Select(x => new StudentFacultyVM()
            {
                ID = x.ID.ToString().ToLower(),
                Name = x.Name,
                FatherName = x.FatherName,
                Identification = x.Identification,
                CNIC = x.CNIC,
                UserType = x.UserType,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Email = x.Email,
                ContactNumber = x.ContactNumber,
                HomeAddress = x.HomeAddress,
                OtherContactNo = x.OtherContactNo,
                BatchNumber = x.BatchNumber,

            })
                .OrderBy(o => o.Name)
                .Skip(((int)page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            return records;
        }
        public async Task<StudentFacultyVM> GetStudent_FacultyByIDAsync(Guid id)
        {

            StudentFacultyVM _Student = new StudentFacultyVM();
            try
            {
                var record = await _context.Student_Faculty.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (record != null)
                {
                    _Student.ID = record.ID.ToString();
                    _Student.Name = record.Name;
                    _Student.FatherName = record.FatherName;
                    _Student.CNIC = record.CNIC;
                    _Student.UserType = record.UserType;
                    _Student.StartDate = record.StartDate;
                    _Student.EndDate = record.EndDate;
                    _Student.Email = record.Email;
                    _Student.ContactNumber = record.ContactNumber;
                    _Student.OtherContactNo = record.OtherContactNo;
                    _Student.HomeAddress = record.HomeAddress;
                    _Student.BatchNumber = record.BatchNumber;
                    return _Student;
                }
                else
                {
                    _Student.StatusCode = "OK";
                    _Student.StatusMsg = "No Such Language Found";
                    return _Student;

                }
            }
            catch (Exception ex)
            {
                _Student.StatusCode = "error";
                _Student.StatusMsg = ex.Message;
                return _Student;
            }

        }

        private async Task<StudentFaculty> GetStudent_FacultyByIDAsync_d(Guid id)
        {
            var records = await _context.Student_Faculty.Where(x => x.ID == id).FirstOrDefaultAsync();

            return records;

        }
    }
}