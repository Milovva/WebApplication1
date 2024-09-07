namespace WebApplication1.Middleware
{
    public class RangeMiddleware
    {
        private readonly RequestDelegate _next;

        public RangeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (int.TryParse(context.Request.Query["number"], out int number) && number >= 1 && number <= 100000)
            {
                context.Items["number"] = number;
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Number must be between 1 and 100000");
            }
        }
    }
}
