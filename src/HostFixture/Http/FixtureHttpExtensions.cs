using Microsoft.Extensions.Http;

namespace HostFixture.Http;

public static class FixtureHttpExtensions
{
    public static IHostFixture AddHttpAction(this IHostFixture fixture, Action<IHttpActionBuilder> builder)
    {

        fixture.SourceBuilder
            .ConfigureServices(services
                => services.AddSingleton(builder));

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