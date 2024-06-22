using Microsoft.EntityFrameworkCore;

namespace StockWebApi.Models
{
    public class StockAppContext : DbContext
    {
        public StockAppContext(DbContextOptions<StockAppContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Watchlist> watchlists { get; set; }
        public DbSet<Stock> stocks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Watchlist>()
                .HasKey(w => new { w.UserId, w.StockId });
        }
    }
}
