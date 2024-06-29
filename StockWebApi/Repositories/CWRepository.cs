using Microsoft.EntityFrameworkCore;
using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public class CWRepository : ICWRepository
    {
        private readonly StockAppContext _context;
        public CWRepository(StockAppContext context)
        {
            _context = context;
        }
        public async Task<List<CoveredWarrant>> GetCoveredWarrantsByStockId(int stockId)
        {
            return await _context.coveredWarrants
                .Where(cw => cw.UnderlyingAssetId == stockId)
                .Include(cw => cw.UnderlyingAsset)
                .ToListAsync();
        }
    }
}
