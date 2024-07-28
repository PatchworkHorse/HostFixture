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

    public static IServiceCollection Replace<TService, TInstance> (this IServiceCollection services,
        ServiceLifetime lifetime)
    {
        services.RemoveAll(typeof(TService))
                .Add(new ServiceDescriptor(typeof(TService), typeof(TInstance), lifetime));

        return services;
    }

    public static IServiceCollection Replace<TService>(this IServiceCollection services, 
        Action<IServiceProvider, TService> factory, 
        ServiceLifetime lifetime)
    {
        services.RemoveAll(typeof(TService))
                .Add(new ServiceDescriptor(typeof(TService), factory, lifetime));

        return services;
    }
}