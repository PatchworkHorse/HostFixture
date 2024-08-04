namespace Services;

public interface IHttpService
{
    const string HttpClientName = "DemoHttpClient";
    const string BaseUrl = "https://api.ipify.org";

    Task<string> GetAsync(string url);
}