namespace TestWebApi;

public class WebRequestService : IWebRequestService
{
    HttpClient Client { get; }
    public WebRequestService(HttpClient httpClient)
        => Client = httpClient;

    public async Task<string> GetAsync(string url)
    {
        var response = await Client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}
