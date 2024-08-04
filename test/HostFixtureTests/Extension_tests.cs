
namespace HostFixtureTests;

public class Extension_tests
{
    [Fact]
    public void should_attach_fixture_to_buidlder()
    {
        // Arrange
        var targetBuilder = new HostBuilder();

        // Act
        var fixturedHost = targetBuilder.ConfigureFixture()
            .GenerateFixturedIHost(); 

        // Assert
        Assert.NotNull(fixturedHost);

        Assert.True(false); 

    }
}