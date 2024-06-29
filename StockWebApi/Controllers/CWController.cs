using Microsoft.AspNetCore.Mvc;
using StockWebApi.Services;

namespace StockWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CWController : ControllerBase
    {
        private readonly ICWService _cWService;
        public CWController(ICWService cWService)
        {
            _cWService = cWService;
        }
        [HttpGet("stock/{stockId}")]
        public async Task<IActionResult> GetCoveredWarrantsByStockId(int stockId)
        {
            try
            {
                var coveredWarrants = await _cWService.GetCoveredWarrantsByStockId(stockId);
                return Ok(coveredWarrants);
            }catch (Exception ex)
            {
                return StatusCode(500, $"An error is occured: { ex.Message}");
            }
        }

    }
}
