namespace HostFixture.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Replace(this IServiceCollection services,
        Type serviceType,
        Type instanceType,
        ServiceLifetime lifetime)
    {
        services.RemoveAll(serviceType)
                .Add(new ServiceDescriptor(serviceType, instanceType, lifetime));

        return services;
    }

    public static IServiceCollection Replace<TService, TInstance>(this IServiceCollection services,
        ServiceLifetime lifetime)
    {
        services.RemoveAll(typeof(TService))
                .Add(new ServiceDescriptor(typeof(TService), typeof(TInstance), lifetime));

        return services;
    }

    public static IServiceCollection Replace<TService>(this IServiceCollection services,
        Func<IServiceProvider, TService> factory,
        ServiceLifetime lifetime)
    {
        services.RemoveAll(typeof(TService))
                .Add(new ServiceDescriptor(typeof(TService), 
                    sp => factory.Invoke(sp) ?? throw new InvalidOperationException("The factory method failed to return an instance"), 
                    lifetime));

        return services;
    }

    public static IServiceCollection Replace(this IServiceCollection services,
        Type serviceType,
        object instance,
        ServiceLifetime lifetime)
    {
        services.RemoveAll(serviceType)
                .Add(new ServiceDescriptor(serviceType, sp => instance, lifetime));

        return services;
    }
}