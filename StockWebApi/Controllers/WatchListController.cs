﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StockWebApi.Filters;
using StockWebApi.Services;
using System.Security.Claims;

namespace StockWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchListController : ControllerBase
    {
        //Vì đã lưu Token vào AuthorizationFilterContext trong class JwtAuthorizeAttribute nên cần inject vào để lấy ra 
        private readonly AuthorizationFilterContext _context;
        private readonly IWatchListService _watchListService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;
        public WatchListController(IWatchListService watchListService, AuthorizationFilterContext context, 
            IUserService userService)
        {
            _watchListService = watchListService;
            _context = context;
            _userService = userService;
        }

        [HttpPost("AddStockToWatchList/{stockId}")]
        [JwtAuthorize]
        public async Task<IActionResult> AddStockToWatchList(int stockId)
        {
            if (!int.TryParse(_context.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

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
            return Ok();
        }
    }
}
