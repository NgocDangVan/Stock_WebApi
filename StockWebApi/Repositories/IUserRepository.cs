using StockWebApi.Models;
using StockWebApi.ViewModels;

namespace StockWebApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<User> GetByEmail(string email);
        Task<User?> Create(RegisterViewModel registerViewModel);
        Task<string> Login(LoginViewModel loginViewModel);
    }
}
