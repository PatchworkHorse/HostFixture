namespace HostFixture.Extensions;

public static class HostExtensions
{
    public static IHostFixture ConfigureFixture(this IHostBuilder sourceBuilder)
        => HostFixture.Create(sourceBuilder); 
}