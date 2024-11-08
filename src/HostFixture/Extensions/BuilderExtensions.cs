namespace HostFixture.Extensions;

public static class BuilderExtensions
{
    public static IHostFixture ConfigureFixture(this IHostApplicationBuilder source)
        => new HostFixture<IHostApplicationBuilder>(source); 

    public static IHostFixture ConfigureFixture(this IHostBuilder source)
        => new HostFixture<IHostBuilder>(source);
}