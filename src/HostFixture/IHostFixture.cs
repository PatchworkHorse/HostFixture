namespace HostFixture;

public interface IHostFixture
{
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
    public IHostFixture RegisterSingleton<TService>(Action<IServiceProvider, TService> factory);

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
    public IHostFixture RegisterScoped<TService>(Action<IServiceProvider, TService> factory);

    /// <summary>
    /// Registers a TIstance of TService as Scoped, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <param name="factory">The factory method to create the instance</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    /// <remarks>
    public IHostFixture RegisterScoped<TService>(Func<TService> factory);

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
    public IHostFixture RegisterTransient<TService>(Action<IServiceProvider, TService> factory);


    /// <summary>
    /// Registers a TIstance of TService as Transient, replacing any existing registration regardless of the lifetime.
    /// </summary>
    /// <typeparam name="TService">The type of service being registered. Used to resolve existing registrations</typeparam>
    /// <param name="factory">The factory method to create the instance</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    /// <remarks>
    public IHostFixture RegisterTransient<TService>(Func<TService> factory);

    /// <summary>
    /// Builds and returns an IHost with fixture configurations attached.
    /// </summary>
    public IHost GenerateFixturedIHost();

    public IHostBuilder SourceBuilder { get; set; }
}