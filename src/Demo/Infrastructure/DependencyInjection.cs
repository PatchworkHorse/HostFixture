using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Infrastucture;

public static class DependencyInjection
{
    public static IServiceCollection AddDemoInfrastructure(this IServiceCollection services)
        => services.AddHttpClient(IHttpService.HttpClientName, client =>
        {
            client.BaseAddress = new Uri(IHttpService.BaseUrl);
        })
        .Services;
}