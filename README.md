# HostFixture

## Overview
HostFixture is a .NET library designed to simplify the process of setting up and configuring `IHost` instances for integration testing. 

## Features 
- Fluent API for configuring, registering, and replacing `IServiceCollection` entries.
- Programmable HTTP responses and callbacks for injected `HttpClient` instances, including fluent filtering for both `HttpRequest` and `HttpResponse` objects. 
- Easy integration with existing .NET hosting infrastructure.
- Extensible design to accommodate custom configurations and extensions.

## Installation 
See the Package section of https://github.com/PatchworkHorse/HostFixture for NuGet packages. 

Coming soon: Packages on NuGet.org.

Currently, projects using HostFixture need to expose a method that creates and configures an `IHostBuilder` or `IHostApplicationBuilder`. This usually involves moving the builder setup logic out of the `Main` method so it can be intercepted for integration testing. HostFixture uses the provided instance to access and manipulate the service collection.

## Usage

### Setting Up the Host

In this example, the Program.cs file provides public access to the builder. The `Main` method passes the command-line arguments to the `CreateBuilder` method, which is responsible for setting up the `WebApplicationBuilder`.

```csharp
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Service configuration omitted for brevity

        return builder;
    }


    public static void Main(string[] args)
    {
        var builder = CreateBuilder(args);
        var app = builder.Build();

        // Endpoint mappings omitted for brevity

        app.Run();
    }
```


### Service Replacement & Registration

The core purpose of HostFixture is to provide a fluent API for configuring `IHost` instances for integration testing. Existing or new services can be registered or replaced in the `IServiceCollection` using the `HostFixtureBuilder`, with any lifetime required (e.g., `Singleton`, `Scoped`, `Transient`).

For example, consider a service that calls an external REST API to retrieve the current weather for a given location. During integration testing, accessing this external dependency may not be desirable or possible. Instead, the registered service can be replaced with a mock or stub implementation that satisfies the same interface, allowing the application to function as expected during tests.

From the perspective of the fixtured application, the fact that the dependency has been replaced is transparent. The application code can continue to use the same interfaces and abstractions, while the underlying implementation is swapped out for testing purposes.

```csharp
using HostFixture;
using Microsoft.Extensions.DependencyInjection;

var builder = Program.CreateBuilder(args);

// Create a fixture from your existing builder
var fixture = builder.ConfigureFixture();

// Replace a service with a mock implementation
fixture.RegisterSingleton<IWeatherService>(sp => new MockWeatherService());

// Replace with a specific instance
var mockLogger = new Mock<ILogger<MyService>>();
fixture.RegisterSingleton(typeof(ILogger<MyService>), mockLogger.Object);

// Replace with a factory method
fixture.RegisterScoped<IDataService>(() => new InMemoryDataService());

// Build and use the configured host
var app = builder.Build();
```

Of course, the above an be done fluently:

```csharp
var builder = Program.CreateBuilder(args);  

builder
    .ConfigureFixture()
    .RegisterSingleton<IWeatherService>(sp => new MockWeatherService())
    .RegisterSingleton(typeof(ILogger<MyService>), mockLogger.Object)
    .RegisterScoped<IDataService>(() => new InMemoryDataService());

var app = builder.Build();
```

### HTTP Client Mocking

HostFixture provides powerful HTTP client mocking capabilities, allowing you to intercept and mock HTTP requests made by your application during testing. This is particularly useful for testing applications that depend on external APIs.

```csharp
using HostFixture.Http;
using System.Net;

var builder = Program.CreateBuilder(args);

builder
    .ConfigureFixture()
    .AddHttpAction(action =>
    {
        action
            .WithRequestUriFilter(uri => uri.Host == "api.external-service.com")
            .WithRequestMethodFilter(method => method == HttpMethod.Get)
            .SetResponseCode(HttpStatusCode.OK)
            .SetResponseContent(new StringContent("""
                {
                    "temperature": 72,
                    "condition": "sunny"
                }
                """, Encoding.UTF8, "application/json"))
            .AddRequestCallback(request => 
            {
                // Log or assert on the request
                Console.WriteLine($"Intercepted request to {request.RequestUri}");
            });
    });

var app = builder.Build();

// Now any HTTP requests to api.external-service.com will return the mocked response
```

### Configuration Override

HostFixture allows you to override configuration values for testing purposes. This is useful for testing different scenarios without modifying the actual configuration file. 

At the simplest level, individual configuration elements can be overridden:
```csharp
var builder = Program.CreateBuilder(args);

builder
    .ConfigureFixture()
    .WithConfigElement("ConnectionStrings:DefaultConnection", "InMemoryDatabase"); 

var app = builder.Build();
```

Or, an entire configuration file can be loaded in addition to the existing configuration sources:
```csharp
var builder = Program.CreateBuilder(args);

builder
    .ConfigureFixture()
    .WithConfigFile("appsettings.test.json", optional: true);

var app = builder.Build();
```

Raw JSON strings can be fluently added as well:
```csharp
var builder = Program.CreateBuilder(args);

builder
    .ConfigureFixture()
    .WithConfigJson("""
    {
        "Key1": "Value1",
        "Key2": "Value2"
    }
    """);

var app = builder.Build();
```

Configuration overrides are fully chainable with other config and fixture methods. For configuration changes, last one wins.

```csharp
var builder = Program.CreateBuilder(args);

builder
    .ConfigureFixture()
    .WithConfigElement("ConnectionStrings:DefaultConnection", "InMemoryDatabase")
    .WithConfigElement("Features:NewFeatureEnabled", "true")
    .WithConfigFile("appsettings.test.json", optional: true)
    .WithConfigJson("""
    {
        "Key1": "Value1",
        "Key2": "Value2"
    }
    """);

var app = builder.Build();
```

## API Reference

### Core Methods

#### Service Registration
- `RegisterSingleton<TService, TInstance>()` - Replace service with singleton lifetime
- `RegisterScoped<TService, TInstance>()` - Replace service with scoped lifetime  
- `RegisterTransient<TService, TInstance>()` - Replace service with transient lifetime
- `RegisterSingleton<TService>(Func<TService> factory)` - Register using factory method
- `RegisterSingleton(Type serviceType, object instance)` - Register specific instance

#### HTTP Mocking
- `AddHttpAction(Action<IHttpActionBuilder> builderDelegate)` - Configure HTTP request interception
- `WithRequestUriFilter(Func<Uri, bool> filter)` - Filter by request URI
- `WithRequestMethodFilter(Func<HttpMethod, bool> filter)` - Filter by HTTP method
- `SetResponseCode(HttpStatusCode code)` - Set response status code
- `SetResponseContent(HttpContent content)` - Set response body content
- `AddRequestCallback(Action<HttpRequestMessage> callback)` - Add request callback

#### Configuration
- `WithConfigElement(string key, object? value)` - Override single configuration value
- `WithConfigFile(string path, bool optional)` - Load additional configuration file

## Advanced Usage Examples

### Testing a Web API with External Dependencies

```csharp
[Fact]
public async Task Should_Handle_External_Service_Failure()
{
    // Arrange
    var builder = Program.CreateBuilder(Array.Empty<string>());
    
    builder
        .ConfigureFixture()
        .AddHttpAction(action =>
        {
            action
                .WithRequestUriFilter(uri => uri.Host == "payment-gateway.com")
                .SetResponseCode(HttpStatusCode.ServiceUnavailable);
        })
        .RegisterSingleton<INotificationService>(new MockNotificationService());

    var app = builder.Build();
    var client = new TestServer(app).CreateClient();

    // Act
    var response = await client.PostAsync("/api/orders", 
        new StringContent("{\"productId\": 123}", Encoding.UTF8, "application/json"));

    // Assert
    Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
}
```

### Testing Different Configuration Scenarios

```csharp
[Theory]
[InlineData("development", true)]
[InlineData("production", false)]
public async Task Should_Respect_Environment_Settings(string environment, bool expectedDebugMode)
{
    // Arrange
    var builder = Program.CreateBuilder(Array.Empty<string>());
    
    builder
        .ConfigureFixture()
        .WithConfigElement("Environment", environment)
        .WithConfigElement("Logging:LogLevel:Default", environment == "development" ? "Debug" : "Warning");

    var app = builder.Build();
    var debugService = app.Services.GetRequiredService<IDebugService>();

    // Act & Assert
    Assert.Equal(expectedDebugMode, debugService.IsDebugEnabled);
}
```

## Testing Best Practices

### 1. **Isolate External Dependencies**
Always mock external HTTP calls, databases, and third-party services to ensure your tests are reliable and fast.

```csharp
// Good: Mock external dependencies
fixture.AddHttpAction(action => 
    action.WithRequestUriFilter(uri => uri.Host.Contains("external"))
          .SetResponseCode(HttpStatusCode.OK));

// Good: Use in-memory implementations
fixture.RegisterSingleton<IRepository>(new InMemoryRepository());
```

### 2. **Test Configuration Variations**
Use HostFixture's configuration override capabilities to test different scenarios.

```csharp
// Test with feature flags
fixture.WithConfigElement("Features:NewPaymentFlow", "true");

// Test with different connection strings
fixture.WithConfigElement("ConnectionStrings:Database", testConnectionString);
```

### 3. **Verify Interactions**
Use callbacks to verify that your application makes the expected HTTP requests.

```csharp
var requestsMade = new List<string>();

fixture.AddHttpAction(action =>
    action.AddRequestCallback(req => requestsMade.Add(req.RequestUri.ToString())));

// After test execution, verify requestsMade contains expected URLs
```

## Requirements

- **.NET 8.0** or later
- Your application must expose the `IHostBuilder` or `IHostApplicationBuilder` setup logic (typically by moving it out of the `Main` method)

## Compatibility

HostFixture works with:
- ASP.NET Core Web APIs
- Worker Services  
- Console Applications using Generic Host
- Any .NET application using `IHostBuilder` or `IHostApplicationBuilder`



## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact
For any questions, comments, or feedback, use the contact information listed at https://github.com/PatchworkHorse