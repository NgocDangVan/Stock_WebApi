using Microsoft.EntityFrameworkCore;
using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly StockAppContext _stockAppContext;
        public QuoteRepository(StockAppContext stockAppContext)
        {
            _stockAppContext = stockAppContext;
        }

        public async Task<List<Quote>> GetHistoricalQuotes(int days, int stockId)
        {
            var fromDate = DateTime.Now.Date.AddDays(-days);
            var toDate = DateTime.Now.Date;
            var historicalQuotes = await _stockAppContext.quotes
                                    .Where(q => q.TimeStamp >= fromDate
                                    && q.TimeStamp <= toDate
                                    && q.StockId == stockId)
                                    .GroupBy(q => q.TimeStamp.Date) // Nhóm theo ngày
                                    .Select(g => new Quote
                                    {
                                        TimeStamp = g.Key,
                                        Price = g.Average(q => q.Price) //Lấy giá trị trung bình của cùng một ngày
                                    })
                                    .OrderBy(q => q.TimeStamp)
                                    .ToListAsync();
            return historicalQuotes;
        }

        public async Task<List<RealtimeQuote>?> GetRealtimeQuote(int page, int limit, string sector, string industry)
        {
            var query = _stockAppContext.realtimeQuotes
                                .Skip((page - 1) * limit)
                                .Take(limit);
            if(!string.IsNullOrEmpty(sector) )
            {
                query = query.Where(q => (q.Sector ?? "").ToLower().Equals(sector.ToLower()));
            }

            if (!string.IsNullOrEmpty(industry))
            {
                query = query.Where(q => (q.Industry ?? "").ToLower().Equals(industry.ToLower()));
            }
            var quotes = await query.ToListAsync();
            return quotes;
        }
    }
}
