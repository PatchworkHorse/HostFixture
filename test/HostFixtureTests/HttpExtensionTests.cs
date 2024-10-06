using FluentAssertions;
using Moq;
using TestWebApi;
using Microsoft.Extensions.DependencyInjection;
using HostFixture.Http;
using System.Reflection.Metadata;
using System.Net;


namespace HostFixtureTests;

public class HttpExtensionTests
{

    [Fact]
    public void should_add_http_action_handler()
    {
        // Arrange

        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());

        var fixturedHost = targetBuilder.ConfigureFixutre()
            .AddHttpAction(b =>
            {
                // For any outgoing HTTP request to https://www.github.com, return a mocked response

                b.WithRequestUriFilter((uri) => uri.ToString() == "https://www.github.com")
                 .SetResponseContent(new StringContent("I'm a mocked response!"))
                 .SetResponseCode(HttpStatusCode.OK);
            })
            .GenerateFixturedIHost();

        // Assert
        // We expect our mocked IStringService to be resolved as a StringService
        var handler = fixturedHost.Services.GetRequiredService<HttpActionHandler>();
        Assert.NotNull(handler);
    }
}

