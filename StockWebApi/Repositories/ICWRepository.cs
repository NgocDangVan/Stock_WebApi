using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public interface ICWRepository
    {
        Task<List<CoveredWarrant>> GetCoveredWarrantsByStockId(int stockId);
    }
}
