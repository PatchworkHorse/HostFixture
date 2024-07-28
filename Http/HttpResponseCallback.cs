namespace HostFixture.Http;

public class HttpResponseCallback(Action<HttpResponseMessage> callback)
{
    Action<HttpResponseMessage> Callback { get; } = callback;

    internal static HttpResponseCallback Create(Action<HttpResponseMessage> callback)
        => new HttpResponseCallback(callback);
}