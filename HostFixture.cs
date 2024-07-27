
using HostFixture.Extensions;

namespace HostFixture;


public class HostFixture(IHostBuilder sourceBuilder) : IHostFixture 
{
 
    IHostBuilder SourceBuilder { get; } = sourceBuilder;

    public IHost BuildFixturedHost()
        => SourceBuilder.ConfigureServices((context, services) =>
        {
            // Todo: interceptors
        }).Build();

    public IHostFixture RegisterScoped<TService, TInstance>()
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Scoped));

        return this;
    }

    public IHostFixture RegisterScoped<TService>(Action<IServiceProvider, TService> factory)
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace(factory, ServiceLifetime.Scoped));

        return this;
    }
    

    public IHostFixture RegisterSingleton<TService, TInstance>()
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Singleton));

        return this;
    }

    public IHostFixture RegisterSingleton<TService>(Action<IServiceProvider, TService> factory)
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace(factory, ServiceLifetime.Singleton));

        return this;
    }

    public IHostFixture RegisterTransient<TService, TInstance>()
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Transient));

        return this;
    }

    public IHostFixture RegisterTransient<TService>(Action<IServiceProvider, TService> factory)
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace(factory, ServiceLifetime.Transient));

        return this;
    }

}