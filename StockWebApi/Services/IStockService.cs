using StockWebApi.Models;

namespace StockWebApi.Services
{
    public interface IStockService
    {
        Task<Stock?> GetStockById(int stockId);
    }
}
