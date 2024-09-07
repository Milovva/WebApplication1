using Humanizer;

namespace WebApplication1.Middleware
{
    public class NumberToWordsMiddleware
    {
        private readonly RequestDelegate _next;

        public NumberToWordsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Items.TryGetValue("number", out var numberObj) && numberObj is int number)
            {
                var numberInWords = number.ToWords();
                context.Items["numberInWords"] = numberInWords;
            }

            await _next(context);
        }
    }
}
