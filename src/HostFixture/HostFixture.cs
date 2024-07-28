/*
Todo: 
"Create" isn't defined on IHostFixture, are we defeating the purpose of the interface?
*/

using HostFixture.Extensions;

namespace HostFixture;


public class HostFixture(IHostBuilder sourceBuilder) : IHostFixture 
{
 
    IHostBuilder SourceBuilder { get; set; } = sourceBuilder;

    public IHost BuildFixturedHost()
        => SourceBuilder.ConfigureServices((context, services) =>
        {
            // Todo: interceptors
        }).Build();

    public static IHostFixture Create(IHostBuilder sourceBuilder)
        => new HostFixture(sourceBuilder); 

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