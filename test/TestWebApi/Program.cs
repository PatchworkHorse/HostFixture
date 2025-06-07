using TestWebApi;
using TestWebApi.Services;
using TestWebApi.Config;

public static class Program
{
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder
         .Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); 
         
        builder
         .Services
         .Configure<ApiConfig>(builder.Configuration.GetSection(nameof(ApiConfig)))
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
        app.MapGet("/string/config", (IStringService service) => service.ReturnFromConfig());

        // Using the web request service
        app.MapGet("/web", async (IWebRequestService service, string url) => await service.GetAsync(url));

        app.Run();
    }
}