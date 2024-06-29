using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace StockWebApi.Controllers
{
    [Route("api/ws")]
    public class WebSocketController : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                // Sinh ngẫu nhiên 2 giá trị x, y, thay đổi 2 giây / lần
                var random = new Random();
                while (webSocket.State == System.Net.WebSockets.WebSocketState.Open)
                {
                    int x = random.Next(1, 100);
                    int y = random.Next(1, 100);
                    var buffer = Encoding.UTF8.GetBytes($"{{ \"x\": {x}, \"y\": {y}}}");
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(buffer),
                        System.Net.WebSockets.WebSocketMessageType.Text,true, CancellationToken.None
                        );
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
    }
}
