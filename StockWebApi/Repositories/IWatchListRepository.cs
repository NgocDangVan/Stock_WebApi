using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public interface IWatchListRepository
    {
        Task AddStockToWatchList(int userId, int stockId);
        Task<Watchlist?> GetWatchList(int userId, int stockId);
    }
}
