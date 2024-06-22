
using StockWebApi.Models;
using StockWebApi.Repositories;

namespace StockWebApi.Services
{
    public class WatchListService : IWatchListService
    {
        private readonly IWatchListRepository _repository;
        public WatchListService(IWatchListRepository repository) 
        {
            _repository = repository;      
        }
        public async Task AddStockToWatchList(int userId, int stockId)
        {
            await _repository.AddStockToWatchList(userId, stockId);
        }

        public async Task<Watchlist> GetWatchList(int userId, int stockId)
        {
            return await _repository.GetWatchList(userId, stockId);
        }
    }
}
