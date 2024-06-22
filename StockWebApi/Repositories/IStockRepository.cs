using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockById(int stockId);
    }
}
