using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace HostFixtureTests;

public class ConfigManipulationTests
{

    [Fact]
    public void should_add_new_iconfig_values()
    {
        // Arrange

        // Force our config key/values to be unique per run to ensure we're not leaking state from prior runs
        string testKey = Guid.NewGuid().ToString();
        string testValue = Guid.NewGuid().ToString();

        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());

        targetBuilder
            .ConfigureFixture()
            .WithConfigElement(testKey, testValue); // Inject our test IConfiguration elements into the host builder

        // Act
        var fixturedApp = targetBuilder.Build();

        // Assert
        var configValue = fixturedApp.Services.GetRequiredService<IConfiguration>()[testKey];
        Assert.Equal(testValue, configValue);

    }

    [Fact]
    public void should_add_new_iconfig_file()
    {
        // Arrange
        const string testKey = "HorsesAreTheBest";
        const string testValue = "True";

        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());

        targetBuilder
            .ConfigureFixture()
            .WithConfigFile("Assets/appsettings.test.json", optional: false);

        // Act
        var fixturedApp = targetBuilder.Build();

        // Assert
        var configValue = fixturedApp.Services.GetRequiredService<IConfiguration>()[testKey];
        Assert.Equal(testValue, configValue);
    }

    [Fact]
    public void should_override_existing_config_value()
    {
        // Arrange

        // Force our config key/values to be unique per run to ensure we're not leaking state from prior runs
        string testKey = nameof(TestWebApi.Config.ApiConfig.StringToReturn);

        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());
        const string testValue = "I have been overridden by HostFixture"; // This is the value we want to override with

        targetBuilder
            .ConfigureFixture()
            .WithConfigElement(testKey, testValue); // Inject our test IConfiguration elements into the host builder

        // Act
        var fixturedApp = targetBuilder.Build();

        // Assert
        var configValue = fixturedApp.Services.GetRequiredService<IConfiguration>()[testKey];
        Assert.Equal(testValue, configValue);
    }
}

