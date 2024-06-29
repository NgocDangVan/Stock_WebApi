using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StockWebApi.Filters
{
    public class JwtAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        public JwtAuthorizeFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Lấy Token đính ở header để kiểm tra
            var jwtToken = context.HttpContext.Request
                            .Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (jwtToken == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.ReadJwtToken(jwtToken);

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "");

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var token = (JwtSecurityToken)validatedToken;
                if(token.ValidTo < DateTime.UtcNow)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var userId = int.Parse(token.Claims.First().Value);
                context.HttpContext.Items["UserId"] = userId;
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
