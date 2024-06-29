using Microsoft.AspNetCore.Mvc;
using StockWebApi.Models;
using StockWebApi.Services;
using System.Text;
using System.Text.Json;

namespace StockWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet("ws")]
        // https://localhost:7036/api/quote/ws
        public async Task GetRealtimeQuotes(
            int page = 1, 
            int limit = 10,
            string sector = "",
            string industry = "")
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                while (webSocket.State == System.Net.WebSockets.WebSocketState.Open)
                {
                    List<RealtimeQuote>? quotes = await _quoteService.GetRealtimeQuote(page,limit,sector,industry);
                    //Convert List of object to Json
                    string jsonString = JsonSerializer.Serialize(quotes);
                    var buffer = Encoding.UTF8.GetBytes(jsonString);
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer),
                        System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
                    await Task.Delay(2000); // Đợi 2 giây trước khi nhận giá trị tiếp theo

                }
                await webSocket.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure,
                    "Connection closed by the server", CancellationToken.None);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        [HttpGet("historical")]
        public async Task<IActionResult> GetHistoricalQuotes(int days, int stockId)
        {
            var historicalQuotes = await _quoteService.GetHistoricalQuotes(days, stockId);
            return Ok(historicalQuotes);
        }
    }
}
