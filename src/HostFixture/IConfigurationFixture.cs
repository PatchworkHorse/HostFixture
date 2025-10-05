namespace HostFixture;

public interface IConfigurationFixture
{
    /// <summary>
    /// Add a configuration element (key-value pair).
    /// </summary>
    /// <param name="key">The configuration key.</param>
    /// <param name="value">The configuration value.</param>
    /// <returns>The <see cref="IHostFixture"/> instance for chaining.</returns>
    IHostFixture WithConfigElement(string key, object? value);

    /// <summary>
    /// Add configuration from a file.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="optional"></param>
    /// <returns>The <see cref="IHostFixture"/> instance for chaining.</returns>
    IHostFixture WithConfigFile(string path, bool optional = true);

    /// <summary>
    /// Add configuration from a raw JSON string.
    /// </summary>
    /// <param name="rawConfig">The raw JSON string.</param>
    /// <returns>The <see cref="IHostFixture"/> instance for chaining.</returns>
    IHostFixture WithConfigJson(string rawConfig);
}