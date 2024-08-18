namespace jwtApp.Middleware
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
                Console.WriteLine($"Token added to headers: {token}"); // Debug log
            }
            else
            {
                Console.WriteLine("No token found in cookies"); // Debug log
            }

            await _next(context);
        }
    }
}
