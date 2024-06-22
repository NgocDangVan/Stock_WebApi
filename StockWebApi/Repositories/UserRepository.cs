using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockWebApi.Models;
using StockWebApi.ViewModels;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace StockWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StockAppContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(StockAppContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> Create(RegisterViewModel registerViewModel)
        {
            var usernameParam = new SqlParameter("@username", registerViewModel.Username ?? "");
            var passwordParam = new SqlParameter("@password", registerViewModel.Password);
            var emailParam = new SqlParameter("@email", registerViewModel.Email);
            var phoneParam = new SqlParameter("@phone", registerViewModel.Phone ?? "");
            var fullNameParam = new SqlParameter("@full_name", registerViewModel.FullName ?? "");
            var dateOfBirthParam = new SqlParameter("@date_of_birth", registerViewModel.DateOfBirth);
            var countryParam = new SqlParameter("@country", registerViewModel.Country ?? "");

            IEnumerable<User> user = await _context.Users.FromSqlRaw("EXECUTE RegisterUser @username, @password, @email, @phone, @full_name, @date_of_birth, @country",
                usernameParam, passwordParam, emailParam, phoneParam, fullNameParam, dateOfBirthParam, countryParam)
                .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            var passwordParam = new SqlParameter("@password", loginViewModel.Password);
            var emailParam = new SqlParameter("@email", loginViewModel.Email);

            IEnumerable<User> result = await _context.Users.FromSqlRaw("EXECUTE CheckLogin @email, @password",
                emailParam,passwordParam)
                .ToListAsync();

            User? user = result.FirstOrDefault();
            if(result != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                throw new Exception("Không thể tìm thấy user");
            }
        }
    }
}
