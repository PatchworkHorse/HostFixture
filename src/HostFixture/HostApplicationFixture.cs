using HostFixture.Extensions;

namespace HostFixture;

public class HostApplicationFixture 
    : IHostFixture<IHostApplicationBuilder>
{
    public IHostApplicationBuilder SourceBuilder { get; set; }

    public HostApplicationFixture(IHostApplicationBuilder builder)
    {
        SourceBuilder = builder;
    }
    
    public IHostFixture ConfigureServices(Action<IServiceCollection> configDelegate)
    {
        configDelegate.Invoke(SourceBuilder.Services); 

        return this;
    }

    // Scoped replacements

    public IHostFixture RegisterScoped<TService, TInstance>()
    {
        SourceBuilder.Services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Scoped);

        return this;
    }

    public IHostFixture RegisterScoped<TService>(Action<IServiceProvider, TService> factory)
    {
        SourceBuilder.Services.Replace(factory, ServiceLifetime.Scoped);

        return this;
    }

    public IHostFixture RegisterScoped<TService>(Func<TService> factory)
    {
        SourceBuilder.Services.Replace<TService>((sp, TService) => factory.Invoke(), ServiceLifetime.Scoped);

        return this;
    }

    // Singleton replacements

    public IHostFixture RegisterSingleton<TService, TInstance>()
    {
        SourceBuilder.Services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Singleton);

        return this;
    }

    public IHostFixture RegisterSingleton<TService>(Action<IServiceProvider, TService> factory)
    {
        SourceBuilder.Services.Replace(factory, ServiceLifetime.Singleton);

        return this;
    }

    public IHostFixture RegisterSingleton<TService>(Func<TService> factory)
    {
        SourceBuilder.Services.Replace<TService>((sp, TService) => factory.Invoke(), ServiceLifetime.Singleton);

        return this;
    }


    // Transient replacements

    public IHostFixture RegisterTransient<TService, TInstance>()
    {
        SourceBuilder.Services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Transient);

        return this;
    }

    public IHostFixture RegisterTransient<TService>(Action<IServiceProvider, TService> factory)
    {
        SourceBuilder.Services.Replace(factory, ServiceLifetime.Transient);

        return this;
    }

    public IHostFixture RegisterTransient<TService>(Func<TService> factory)
    {
        SourceBuilder.Services.Replace<TService>((sp, TService) => factory.Invoke(), ServiceLifetime.Transient);

        return this;
    }
}