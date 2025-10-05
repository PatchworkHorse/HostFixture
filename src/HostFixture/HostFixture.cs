using HostFixture.Extensions;
using Microsoft.Extensions.Configuration;

namespace HostFixture;

public class HostFixture<TBuilder> : IHostFixture
{

    public IServiceCollection Services { get; set; } = default!;

    public TBuilder Builder { get; set; } = default!;

    public HostFixture(TBuilder builder)
    {
        if (builder is IHostApplicationBuilder appBuilder)
            Services = appBuilder.Services;

        if (builder is IHostBuilder hostBuilder)
            hostBuilder.ConfigureServices((context, services) => Services = services);

        Builder = builder;
    }

    // Entry point for the fixture, allowing configuration of services
    public IHostFixture ConfigureServices(Action<IServiceCollection> serviceCollection)
    {
        serviceCollection(Services);
        return this;
    }

    #region Configuration Overrides
    public IHostFixture WithConfigElement(string key, object? value)
    {
        if (Builder is IHostApplicationBuilder appBuilder)
            appBuilder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>() { { key, value?.ToString() } });

        if (Builder is IHostBuilder hostBuilder)
            hostBuilder.ConfigureAppConfiguration(cb =>
                cb.AddInMemoryCollection(new Dictionary<string, string?>() { { key, value?.ToString() } }));

        return this;
    }

    public IHostFixture WithConfigFile(string path, bool optional = true)
    {
        if (Builder is IHostApplicationBuilder appBuilder)
            appBuilder.Configuration.AddJsonFile(path, optional: optional, reloadOnChange: true);

        if (Builder is IHostBuilder hostBuilder)
            hostBuilder.ConfigureAppConfiguration(cb =>
                cb.AddJsonFile(path, optional: optional, reloadOnChange: true));

        return this;
    }

    public IHostFixture WithJsonConfig(string rawConfig)
    {
        if (Builder is IHostApplicationBuilder appBuilder)
            appBuilder.Configuration.AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(rawConfig)));

        if (Builder is IHostBuilder hostBuilder)
            hostBuilder.ConfigureAppConfiguration(cb =>
                cb.AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(rawConfig))));

        return this;
    }

    #endregion

    #region Scoped Replacements
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

    #endregion

    #region Singleton Replacements

    public IHostFixture RegisterSingleton<TService>(Func<IServiceProvider, TService> factory)
    {
        Services.Replace(factory, ServiceLifetime.Singleton);
        return this;
    }

    public IHostFixture RegisterSingleton(Type serviceType, object instance)
    {
        Services.Replace(serviceType, instance, ServiceLifetime.Singleton);
        return this;
    }

    public IHostFixture RegisterSingleton<TService, TInstance>()
    {
        Services.Replace(typeof(TService), typeof(TInstance), ServiceLifetime.Singleton);
        return this;
    }

    public IHostFixture RegisterSingleton<TService>(Func<TService> factory)
    {
        TService instance = factory();

        if (instance == null)
            throw new InvalidOperationException("The factory method failed to return an instance");

        Services.Replace(typeof(TService), instance, ServiceLifetime.Singleton);
        return this;
    }

    #endregion

    #region Transient Replacements

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
        Services.Replace(factory, ServiceLifetime.Transient);
        return this;
    }

    public IHostFixture RegisterTransient(Type serviceType, object instance)
    {
        Services.Replace(serviceType, instance, ServiceLifetime.Transient);
        return this;
    }

    #endregion
}