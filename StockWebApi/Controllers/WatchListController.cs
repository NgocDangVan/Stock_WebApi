using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StockWebApi.Attributes;
using StockWebApi.Extensions;
using StockWebApi.Filters;
using StockWebApi.Services;
using System.Security.Claims;

namespace StockWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListService _watchListService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;
        public WatchListController(IWatchListService watchListService,  
            IUserService userService,IStockService stockService)
        {
            _watchListService = watchListService;
            _userService = userService;
            _stockService = stockService;
        }

        [HttpPost("AddStockToWatchList/{stockId}")]
        [JwtAuthorize]
        public async Task<IActionResult> AddStockToWatchList(int stockId)
        {
            //Lấy UserId từ context
            int userId = HttpContext.GetUserId();
            var user = await _userService.GetUserById(userId);
            var stock = await _stockService.GetStockById(stockId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (stock == null)
            {
                return NotFound("Stock not found");
            }

            var existingWatchlistItem = await _watchListService.GetWatchList(userId, stockId);
            if (existingWatchlistItem != null) 
            {
                return BadRequest("Stock is already in watchlist.");
            }

            await _watchListService.AddStockToWatchList(userId,stockId);
            return Ok(new
            {
                message = "Insert stock to watch list"
            }) ;
        }
    }
}
