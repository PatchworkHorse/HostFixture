using Microsoft.Extensions.DependencyInjection;
using HostFixture.Http;
using System.Net;
using TestWebApi;


namespace HostFixtureTests;

public class HttpExtensionTests
{

    [Fact]
    public async Task should_add_http_action_handler()
    {
        // Arrange

        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());

        targetBuilder
            .ConfigureFixture()
            .AddHttpAction(b =>
            {
                // For any outgoing HTTP request to https://www.github.com, return a mocked response

                b.WithRequestUriFilter((uri) => uri.ToString() == "https://www.github.com/")
                 .SetResponseContent(new StringContent("I'm a mocked response!"))
                 .SetResponseCode(HttpStatusCode.OK);
            });

        // Act
        var fixturedApp = targetBuilder.Build();

        // Assert
        var actionHandler = fixturedApp
            .Services
            .GetRequiredService<HttpActionHandler>(); 

        Assert.NotNull(actionHandler);

        var httpActions = fixturedApp.Services.GetRequiredService<IEnumerable<IHttpActionBuilder>>(); 
        Assert.NotNull(httpActions); 
        Assert.NotEmpty(httpActions); 
    }

    [Fact]
    public async Task should_execute_http_action_handler()
    {
        // Arrange

        var targetBuilder = Program.CreateBuilder(Array.Empty<string>());

        targetBuilder
            .ConfigureFixture()
            .AddHttpAction(b =>
            {
                // For any outgoing HTTP request to https://www.github.com, return a mocked response

                b.WithRequestUriFilter((uri) => uri.ToString() == "https://www.github.com/")
                 .SetResponseContent(new StringContent("I'm a mocked response!"))
                 .SetResponseCode(HttpStatusCode.OK);
            });

        // Act
        var fixturedApp = targetBuilder.Build();

        // Assert
        var webRequestService = fixturedApp.Services.GetRequiredService<IWebRequestService>(); 
        
        var response = await webRequestService.GetAsync("https://www.github.com");

        Console.WriteLine(response); 
    }

}

