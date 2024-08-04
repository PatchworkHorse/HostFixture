namespace Services;

public class HttpService(IHttpClientFactory clientFactory)
 : IHttpService
{
    HttpClient client = clientFactory.CreateClient(IHttpService.HttpClientName);

    public async Task<string> GetAsync(string url)
        => await client.GetStringAsync(url);    
}