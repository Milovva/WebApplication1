
using Humanizer;

namespace WebApplication1.Middleware
{

        public class NumberDeterminantMiddleware
        {
            private readonly RequestDelegate _next;

            public NumberDeterminantMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                if (int.TryParse(context.Request.Query["number"], out int number))
                {
                    var parity = number % 2 == 0 ? "even" : "odd";
                    context.Items["number"] = number;
                    context.Items["parity"] = parity;
                    await _next(context);
                }
                else
                {
                    await _next(context);
                }
            }
        }
    }
