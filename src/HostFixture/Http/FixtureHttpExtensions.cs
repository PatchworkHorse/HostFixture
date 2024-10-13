using HostFixture.Extensions;
using Microsoft.Extensions.Http;

namespace HostFixture.Http;

public static class FixtureHttpExtensions
{
    public static IHostFixture AddHttpAction(this IHostFixture fixture, Action<IHttpActionBuilder> builderDelegate)
    {
        var builder = new HttpActionBuilder(); 
        builderDelegate(builder); 

        fixture.ConfigureServices(services
            => services
                .AddSingleton<IHttpActionBuilder>(builder)
                .AddHttpActionHandler()); 

        return fixture;
    }

    public static IServiceCollection AddHttpActionHandler(this IServiceCollection services)
    {
        if (services.Any(s => s.ServiceType == typeof(HttpActionHandler)))
            return services;

        services
            .AddSingleton<HttpActionHandler>()
            .ConfigureAll<HttpClientFactoryOptions>(options =>
                options.HttpMessageHandlerBuilderActions.Add(builder =>
                {
                    builder.AdditionalHandlers.Add(services.BuildServiceProvider().GetRequiredService<HttpActionHandler>());
                }));

        return services;
    }

}