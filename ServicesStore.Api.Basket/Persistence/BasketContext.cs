using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Basket.Model;

namespace ServicesStore.Api.Basket.Persistence
{
    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> options) : base(options) { }

        public DbSet<BasketSession> BasketSession { get; set; }
        public DbSet<BasketSessionDetail> BasketSessionDetail { get; set; }


    }
}
