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

    [Fact]
    public void should_support_raw_json_config()
    {
        // Arrange
        const string testKey = "HorsesAreTheBest";
        const string testValue = "True";

        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());

        targetBuilder
            .ConfigureFixture()
            .WithJsonConfig("""
            {
                "HorsesAreTheBest": "True"
            }
            """);

        // Act
        var fixturedApp = targetBuilder.Build();

        // Assert
        var configValue = fixturedApp.Services.GetRequiredService<IConfiguration>()[testKey];
        Assert.Equal(testValue, configValue);
    }

    // We should be able to mix and match config override methods, chaining them as needed. 

    [Fact]
    public void should_support_multiple_config_methods()
    {
        // Arrange
        const string fictionalKey = "ClientID";
        Guid fictionalValue = Guid.NewGuid();

        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());

        targetBuilder
            .ConfigureFixture()
            .WithConfigElement(fictionalKey, fictionalValue)
            .WithJsonConfig("""
            {
                "HorsesAreTheBest": "True"
            }
            """);

        // Act
        var fixturedApp = targetBuilder.Build();

        // Assert
        var singleElementConfigValue = fixturedApp.Services.GetRequiredService<IConfiguration>()[fictionalKey];
        Assert.Equal(fictionalValue.ToString(), singleElementConfigValue);

        var rawJsonConfigValue = fixturedApp.Services.GetRequiredService<IConfiguration>()["HorsesAreTheBest"];
        Assert.Equal("True", rawJsonConfigValue);
    }

    // Config chains should be exclusive and depend on order of execution. Last one wins. 

    [Fact]
    public void method_chaining_should_be_exclusive()
    {
        // Arrange
        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());

        var fixture = targetBuilder.ConfigureFixture();

        // Act
        fixture.WithConfigElement("Key1", "FirstValue")
                .WithConfigElement("Key1", "SecondValue")
                .WithConfigElement("Key1", "ThirdValue");
        var fixturedApp = targetBuilder.Build();

        // Assert
        Assert.Equal("ThirdValue", fixturedApp.Services.GetRequiredService<IConfiguration>()["Key1"]);
    }


}

