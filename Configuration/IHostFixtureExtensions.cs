namespace HostFixture.Configuration; 

public static class FixtureHttpExtensions
{
    public static IHostFixture AddHttpResponse(this IHostFixture fixture, Action<IHttpActionBuilder> action)
    {
        // Todo: The implementation.
        return fixture;
    }
}