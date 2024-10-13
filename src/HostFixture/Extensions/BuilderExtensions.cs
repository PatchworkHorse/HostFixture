namespace HostFixture.Extensions;

public static class BuilderExtensions
{
    public static IHostFixture<IHostApplicationBuilder> ConfigureFixture(this IHostApplicationBuilder source)
        => new HostApplicationFixture(source);

    public static IHostFixture<IHostBuilder> ConfigureFixture(this IHostBuilder source)
        => new HostFixture(source);
}