﻿using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public interface IQuoteRepository
    {
        Task<List<RealtimeQuote>?> GetRealtimeQuote(
            int page,
            int limit,
            string sector,
            string industry);
        //Làm thế nào lấy ra số lượng quotes real-time của 3 ngày, 7 ngày, 30 ngày trước ?
        Task<List<Quote>> GetHistoricalQuotes(int days, int stockId);
    }
}
