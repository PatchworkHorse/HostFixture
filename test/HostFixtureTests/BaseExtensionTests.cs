using FluentAssertions;

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

}