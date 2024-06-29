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
        public DbSet<RealtimeQuote> realtimeQuotes { get; set; }
        public DbSet<Quote> quotes { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<CoveredWarrant> coveredWarrants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Watchlist>()
                .HasKey(w => new { w.UserId, w.StockId });
            //Bảng nào có trigger thì phải thông báo, nếu không sẽ báo lỗi
            modelBuilder.Entity<Order>().ToTable(table => table.HasTrigger("orders_trigger"));
        }
    }
}
