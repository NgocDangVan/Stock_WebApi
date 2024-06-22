using StockWebApi.Models;
using StockWebApi.Repositories;

namespace StockWebApi.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<Stock?> GetStockById(int stockId)
        {
            return await _stockRepository.GetStockById(stockId);
        }
    }
}
