namespace HostFixture;

public interface IHostFixture
{
    public IHostFixture ConfigureServices(Action<IServiceCollection> serviceCollection);

    /// <summary>
    /// Registers a TIstance of TService as Singleton, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <typeparam name="TInstance">The instance type being added to the service collection</typeparam>
    /// <returns>This IHostFixture to chain additional commands</returns>
    public IHostFixture RegisterSingleton<TService, TInstance>();

    /// <summary>
    /// Registers a TIstance of TService as Singleton, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <param name="factory">The factory method to create the instance</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    /// <remarks>
    public IHostFixture RegisterSingleton<TService>(Func<TService> factory);

    /// <summary>
    /// Registers a TIstance of TService as Singleton, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <param name="factory">The factory method to create the instance</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    /// <remarks>
    public IHostFixture RegisterSingleton<TService>(Func<IServiceProvider, TService> factory);


    /// <summary>
    /// Registers a provided instance as Singleton, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <param name="serviceType">The type of service being registered. Used to resolve existing registrations</param>
    /// <param name="instance">The instance being added to the service collection</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    public IHostFixture RegisterSingleton(Type serviceType, object instance);

    /// <summary>
    /// Registers a TIstance of TService as Scoped, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <typeparam name="TInstance">The instance type being added to the service collection</typeparam>
    /// <returns>This IHostFixture to chain additional commands</returns>
    public IHostFixture RegisterScoped<TService, TInstance>();

    /// <summary>
    /// Registers a TIstance of TService as Scoped, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <param name="factory">The factory method to create the instance</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    /// <remarks>
    public IHostFixture RegisterScoped<TService>(Func<TService> factory);

    /// <summary>
    /// Registers a TIstance of TService as Scoped, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <param name="factory">The factory method to create the instance</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    /// <remarks>
    public IHostFixture RegisterScoped<TService>(Func<IServiceProvider, TService> factory);

    /// <summary>
    /// Registers a provided instance as a Scoped, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <param name="serviceType">The type of service being registered. Used to resolve existing registrations</param>
    /// <param name="instance">The instance being added to the service collection</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    public IHostFixture RegisterScoped(Type serviceType, object instance);

    /// <summary>
    /// Registers a TIstance of TService as Transient, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <typeparam name="TInstance">The instance type being added to the service collection</typeparam>
    /// <returns>This IHostFixture to chain additional commands</returns>
    public IHostFixture RegisterTransient<TService, TInstance>();

    /// <summary>
    /// Registers a TIstance of TService as Transient, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <param name="factory">The factory method to create the instance</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    /// <remarks>
    public IHostFixture RegisterTransient<TService>(Func<TService> factory);

    /// <summary>
    /// Registers a TIstance of TService as Transient, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <param name="factory">The factory method to create the instance</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    /// <remarks>
    public IHostFixture RegisterTransient<TService>(Func<IServiceProvider, TService> factory);


    /// <summary>
    /// Registers a provided instance as Transient, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <param name="serviceType">The type of service being registered. Used to resolve existing registrations</param>
    /// <param name="instance">The instance being added to the service collection</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    public IHostFixture RegisterTransient(Type serviceType, object instance);

}

// public interface IHostFixture<T> : IHostFixture
//     where T : class
// {
//     public T SourceBuilder { get; }
// }