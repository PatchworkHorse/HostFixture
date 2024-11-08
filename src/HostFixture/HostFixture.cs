using HostFixture.Extensions;

namespace HostFixture;

public class HostFixture<TBuilder> : IHostFixture
{    
    
    public IServiceCollection Services { get; set; } = default!;

    public TBuilder Builder { get; set; } = default!;

    public HostFixture(TBuilder builder)
    {
        if(builder is IHostApplicationBuilder appBuilder)
            Services = appBuilder.Services;
        
        if(builder is IHostBuilder hostBuilder)
            hostBuilder.ConfigureServices((context, services) => Services = services);

        Builder = builder;
    }

    public IHostFixture ConfigureServices(Action<IServiceCollection> serviceCollection)
    {
        serviceCollection(Services);
        return this;
    }

    //
    // Scoped replacements
    //
    public IHostFixture RegisterScoped<TService, TInstance>()
    {
        Services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Scoped); 
        return this;
    }

    public IHostFixture RegisterScoped<TService>(Func<TService> factory)
    {
        TService instance = factory();

        if (instance == null)
            throw new InvalidOperationException("The factory method failed to return an instance");

        Services.Replace(typeof(TService), instance, ServiceLifetime.Scoped);
        return this;
    }

    public IHostFixture RegisterScoped<TService>(Func<IServiceProvider, TService> factory)
    {
        Services.Replace(factory, ServiceLifetime.Scoped);
        return this;
    }

    public IHostFixture RegisterScoped(Type serviceType, object instance)
    {
        Services.Replace(serviceType, instance, ServiceLifetime.Scoped); 
        return this;
    }


    // Singleton replacements

    public IHostFixture RegisterSingleton<TService, TInstance>()
    {
        Services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Singleton); 
        return this;
    }

    public IHostFixture RegisterSingleton<TService>(Func<TService> factory)
    {
        TService instance = factory();

        if(instance == null)
            throw new InvalidOperationException("The factory method failed to return an instance");

        Services.Replace(typeof(TService), instance, ServiceLifetime.Singleton); 
        return this;
    }


    public IHostFixture RegisterSingleton<TService>(Func<IServiceProvider, TService> factory)
    {
        Services.Replace(factory, ServiceLifetime.Scoped); 
        return this;
    }

    public IHostFixture RegisterSingleton(Type serviceType, object instance)
    {
        Services.Replace(serviceType, instance, ServiceLifetime.Singleton); 
        return this;
    }



    // Transient replacements

    public IHostFixture RegisterTransient<TService, TInstance>()
    {
        Services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Transient); 
        return this;
    }

    public IHostFixture RegisterTransient<TService>(Func<TService> factory)
    {
        TService instance = factory();

        if (instance == null)
            throw new InvalidOperationException("The factory method failed to return an instance");

        Services.Replace(typeof(TService), instance, ServiceLifetime.Transient);
        return this;
    }

    public IHostFixture RegisterTransient<TService>(Func<IServiceProvider, TService> factory)
    {
        Services.Replace(factory, ServiceLifetime.Scoped); 
        return this;
    }

    public IHostFixture RegisterTransient(Type serviceType, object instance)
    {
        Services.Replace(serviceType, instance, ServiceLifetime.Transient); 
        return this;
    }
}