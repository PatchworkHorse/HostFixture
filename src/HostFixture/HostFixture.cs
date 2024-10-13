using HostFixture.Extensions;

namespace HostFixture;

public class HostFixture
    : IHostFixture<IHostBuilder>
{
    public IHostBuilder SourceBuilder { get; set; } = default!;

    public HostFixture(IHostBuilder builder)
    {
        SourceBuilder = builder;
    }

    public IHostFixture ConfigureServices(Action<IServiceCollection> configDelegate)
    {
        SourceBuilder.ConfigureServices(configDelegate);
        return this;
    }

    // Scoped replacements

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

    public IHostFixture RegisterScoped<TService>(Func<TService> factory)
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace<TService>((sp, TService) => factory.Invoke(), ServiceLifetime.Scoped));

        return this;
    }

    // Singleton replacements

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

    public IHostFixture RegisterSingleton<TService>(Func<TService> factory)
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace<TService>((sp, TService) => factory.Invoke(), ServiceLifetime.Singleton));

        return this;
    }


    // Transient replacements

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

    public IHostFixture RegisterTransient<TService>(Func<TService> factory)
    {
        SourceBuilder.ConfigureServices(services
            => services.Replace<TService>((sp, TService) => factory.Invoke(), ServiceLifetime.Transient));

        return this;
    }
}