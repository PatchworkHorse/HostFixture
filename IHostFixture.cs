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
    /// Sets the specified IConfiguration value at the specified path
    /// </summary>
    /// <param name="path">The path to the configuration value</param>
    /// <param name="value">The value to set</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    public IHostFixture SetConfigurationElement(string path, object? value); 

}