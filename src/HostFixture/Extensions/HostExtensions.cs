using Microsoft.AspNetCore.Builder;

namespace HostFixture.Extensions;

public static class HostExtensions
{
    public static IHostFixture ConfigureFixture(this IHostBuilder sourceBuilder)
        => HostFixture.Create(sourceBuilder); 

    public static IHostFixture ConfigureFixutre(this WebApplicationBuilder sourceBuilder)
        => HostFixture.Create(sourceBuilder.Host);
}