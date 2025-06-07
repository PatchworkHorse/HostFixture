namespace HostFixture;

public interface IConfigurationFixture
{
    IHostFixture WithConfigElement(string key, object? value);

    IHostFixture WithConfigFile(string path, bool optional = true); 
}