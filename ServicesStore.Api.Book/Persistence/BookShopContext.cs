using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Book.Model;

namespace ServicesStore.Api.Book.Persistence
{
    public class BookShopContext: DbContext
    {
        public BookShopContext() {}
        public BookShopContext(DbContextOptions<BookShopContext> options) : base(options) { }
        
        public virtual DbSet<BookShopItem> BookShopItem { get; set; }
    }
}
