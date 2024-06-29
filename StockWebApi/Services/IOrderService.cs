using StockWebApi.Models;
using StockWebApi.ViewModels;

namespace StockWebApi.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrder(OrderViewModel orderViewModel);
        Task<List<Order>> GetOrderHistory();
    }
}
