using StockWebApi.Models;
using StockWebApi.ViewModels;

namespace StockWebApi.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(OrderViewModel orderViewModel);
    }
}
