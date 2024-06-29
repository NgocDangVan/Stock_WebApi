using StockWebApi.Models;

namespace StockWebApi.Services
{
    public interface IQuoteService
    {
        Task<List<RealtimeQuote>?> GetRealtimeQuote(
            int page,
            int limit,
            string sector,
            string industry);
        Task<List<Quote>> GetHistoricalQuotes(int days, int stockId);
    }
}
