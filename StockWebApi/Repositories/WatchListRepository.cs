
using Microsoft.EntityFrameworkCore;
using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly StockAppContext _context;
        private readonly IConfiguration _configuration;
        public WatchListRepository(StockAppContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task AddStockToWatchList(int userId, int stockId)
        {
            // Kiểm tra xem cổ phiếu đã có trong danh sách theo dõi của người dùng chưa
            var watchlistExists = await _context.watchlists.FindAsync(userId,stockId);

            if (watchlistExists != null)
            {
                // Thêm cổ phiếu vào danh sách theo dõi
                var watchlist = new Watchlist
                {
                    UserId = userId,
                    StockId = stockId
                };

                _context.watchlists.Add(watchlist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Watchlist?> GetWatchList(int userId, int stockId)
        {
            return await _context.watchlists
                .FirstOrDefaultAsync(watchlist => watchlist.UserId == userId
                   && watchlist.StockId == stockId);
        }
    }
}
