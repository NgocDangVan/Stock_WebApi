using StockWebApi.Models;

namespace StockWebApi.Services
{
    public interface ICWService
    {
        Task<List<CoveredWarrant>> GetCoveredWarrantsByStockId(int stockId);
    }
}
