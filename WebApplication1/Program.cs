using WebApplication1.Middleware;
namespace WebApplication1
    
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseMiddleware<SentenceLengthMiddleware>();
            //app.UseMiddleware<NumberDeterminantMiddleware>();
            //app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseMiddleware<RoutingMiddleware>();
            //app.Environment.EnvironmentName = "Production";

            //if (app.Environment.IsDevelopment())
            //{
            //    app.Run(async (context) => await context.Response.WriteAsync("In Development Stage"));
            //}
            //else if (app.Environment.IsProduction())
            //{
            //    app.Run(async (context) => await context.Response.WriteAsync("In Production Stage"));
            //}
            //else if (app.Environment.IsEnvironment("Testing"))
            //{
            //    app.Run(async (context) => await context.Response.WriteAsync("In Testing Stage"));
            //}
            // ����������� middleware-�����������
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<RangeMiddleware>(); // ��������� ���� middleware ��� �������� ��������� �����
            app.UseMiddleware<NumberDeterminantMiddleware>();
            app.UseMiddleware<NumberToWordsMiddleware>(); // ��������� ���� middleware ��� �������������� ����� � �����

            app.MapGet("/interpret", async context =>
            {
                if (context.Items.TryGetValue("number", out var numberObj) && numberObj is int number &&
                    context.Items.TryGetValue("parity", out var parityObj) && parityObj is string parity &&
                    context.Items.TryGetValue("numberInWords", out var numberInWordsObj) && numberInWordsObj is string numberInWords)
                {
                    var response = new
                    {
                        //Number = number,
                        //Parity = parity,
                        NumberInWords = numberInWords
                    };

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(response);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid request");
                }
            });

            app.Run();
        }
    }
}
