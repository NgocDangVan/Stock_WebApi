using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StockWebApi.Filters
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {
        public JwtAuthorizeAttribute() : base(typeof(IAuthorizationFilter)) 
        { 
            
        }
        private class JwtAuthorizeFilter : IAuthorizationFilter
        {
            private readonly IConfiguration _configuration;
            public JwtAuthorizeFilter(IConfiguration configuration) {
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
                    var userId = int.Parse(token.Claims.First(x => x.Type == "userId").Value);
                    context.HttpContext.Items["UserId"] = userId;
                }
                catch (Exception ex)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                //var expClaim = token.Claims.FirstOrDefault(c => c.Type == "exp");
                //if (expClaim != null && long.TryParse(expClaim.Value, out var exp))
                //{
                //    var expirationTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(exp).ToLocalTime();
                //    if (expirationTime >= DateTime.Now)
                //    {

                //    }
                //    else
                //    {
                //        context.Result = new UnauthorizedResult();
                //        return;
                //    }
                //}
                //else
                //{
                //    context.Result = new UnauthorizedResult();
                //    return;
                //}
            }
        }
        
    }
}
