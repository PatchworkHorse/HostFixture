namespace TestWebApi;

public interface IWebRequestService
{
    public Task<string> GetAsync(string url);
}