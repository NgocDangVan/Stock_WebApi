namespace StockWebApi.Extensions
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            return httpContext.Items["UserId"] as int? ?? throw new Exception("User Id not found in httpContext.Items");
        }
    }
}
