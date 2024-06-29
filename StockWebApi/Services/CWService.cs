using StockWebApi.Models;
using StockWebApi.Repositories;

namespace StockWebApi.Services
{
    public class CWService : ICWService
    {
        private readonly ICWRepository _cWRepository;
        public CWService(ICWRepository cWRepository)
        {
            _cWRepository = cWRepository;
        }
        public async Task<List<CoveredWarrant>> GetCoveredWarrantsByStockId(int stockId)
        {
            return await _cWRepository.GetCoveredWarrantsByStockId(stockId);
        }
    }
}
