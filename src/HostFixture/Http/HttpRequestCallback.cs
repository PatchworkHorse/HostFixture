namespace HostFixture.Http;

public class HttpRequestCallback(Action<HttpRequestMessage> callback)
{
    Action<HttpRequestMessage> Callback { get; } = callback;

    internal static HttpRequestCallback Create(Action<HttpRequestMessage> callback)
        => new HttpRequestCallback(callback);    
}
