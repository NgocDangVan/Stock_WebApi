using StockWebApi.Models;

namespace StockWebApi.Services
{
    public interface IWatchListService
    {
        Task AddStockToWatchList(int userId, int stockId);
        Task<Watchlist> GetWatchList(int userId, int stockId);
    }
}
