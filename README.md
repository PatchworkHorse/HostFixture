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

The core purpose of HostFixture is to provide a fluent API for configuring `IHost` instances. Existing or new services can be registered or replaced in the `IServiceCollection` using the `HostFixtureBuilder`, with any lifetime required (e.g., `Singleton`, `Scoped`, `Transient`).

For example, consider a service that calls an external REST API to retrieve the current weather for a given location. During integration testing, accessing this external dependency may not be desirable or possible. Instead, the registered service can be replaced with a mock or stub implementation that satisfies the same interface, allowing the application to function as expected during tests.

From the perspective of the fixtured application, the fact that the dependency has been replaced is transparent. The application code can continue to use the same interfaces and abstractions, while the underlying implementation is swapped out for testing purposes.

```csharp
using HostFixture;

// todo: More. 
```



## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact
For any questions, comments, or feedback, use the contact information listed at https://github.com/PatchworkHorse