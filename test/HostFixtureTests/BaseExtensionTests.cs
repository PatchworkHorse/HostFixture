using FluentAssertions;
using Moq;
using TestWebApi;
using Microsoft.Extensions.DependencyInjection;

namespace HostFixtureTests;

public class BaseExtensionTests
{
    // Very simple test to ensure that the extension method is attached to a generic HostBuilder
    [Fact]
    public void should_attach_fixture_to_buidlder()
    {
        // Arrange
        var builder = new HostBuilder();

        builder.ConfigureFixture(); 

        // Assert
        Assert.NotNull(builder);

        builder.Should().NotBeNull();
    }

    [Fact]
    public void should_replace_service_collection_entries()
    {
        // Arrange

        // Grab the IHostBuilder property from the Program class of the SUT project 
        var builder = Program.CreateBuilder(Array.Empty<string>());

        // Act
        builder
            .ConfigureFixture()
            .RegisterTransient(() => 
            {
                // Create and register mock services fluently!!

                var mock = new Mock<IStringService>();
                mock.Setup(x => x.GenerateRandomString(It.IsAny<int>())).Returns("I'm a mocked service!");
                return mock.Object;
            }); 

        var fixturedHost = builder.Build();

        // Assert
        // We expect our mocked IStringService to be resolved as a StringService
        var service = fixturedHost.Services.GetRequiredService<IStringService>();
        Assert.NotNull(service);
    }
}