using TestWebApi;

public static class Program
{
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder
         .Services
         .AddHttpClient()
         .AddTransient<IStringService, StringService>()
         .AddTransient<IWebRequestService, WebRequestService>(); 

        return builder;
    }


    public static void Main(string[] args)
    {
        var builder = CreateBuilder(args);
        var app = builder.Build();

        // Static return value
        app.MapGet("/", () => "Hello World!");

        // Using the string service
        app.MapGet("/string/random", (IStringService service) => service.GenerateRandomString(10));
        app.MapGet("/string/reverse", (IStringService service, string input) => service.ReverseString(input));

        // Using the web request service
        app.MapGet("/web", async (IWebRequestService service, string url) => await service.GetAsync(url));

        app.Run();
    }
}