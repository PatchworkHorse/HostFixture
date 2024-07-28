using HostFixture;

public static class FixtureConfigurationExtensions
{
    /// <summary>
    /// Sets the specified IConfiguration value at the specified path
    /// </summary>
    /// <param name="path">The path to the configuration value</param>
    /// <param name="value">The value to set</param>
    /// <returns>This IHostFixture to chain additional commands</returns>
    public static IHostFixture SetConfigurationElement(this IHostFixture fixture, string path, object? value)
    {
        // Todo: The implementation.
        return fixture; 
    }
}