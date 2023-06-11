using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Author.Model;

namespace ServicesStore.Api.Author.Persistance
{
    public class AuthorContext : DbContext
    {
        public AuthorContext(DbContextOptions<AuthorContext> options) : base(options)
        {

        }

        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<AcademicGrade> AcademicGrades { get; set; }

    }
}
