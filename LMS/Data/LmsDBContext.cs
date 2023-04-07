using LMS.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LmsDBContext : IdentityDbContext<ApplicationUser>
    {
        public LmsDBContext(DbContextOptions<LmsDBContext> options) : base(options) 
        {



        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookLedger> BookLedgers { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentFaculty> Student_Faculty { get; set; }
    }
}
