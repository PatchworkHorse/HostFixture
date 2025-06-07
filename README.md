# HostFixture

## Overview
HostFixture is a .NET library designed to simplify the process of setting up and configuring `IHost` instances for integration testing. 

## Features 
- Fluent API for configuring, registering, and replacing `IServiceCollection` entries.
- Programmable HTTP responses and callbacks for injected `HttpClient` instances, including fluent filtering for both `HttpRequest` and `HttpResponse` objects. 
- Easy integration with existing .NET hosting infrastructure.
- Extensible design to accommodate custom configurations and extensions.

## Installation 
Currently, there is no package generated for this project; this is in the near-term to-do. To try it out, clone this repo and reference the namespaces directly.

## Usage

### Service Replacement & Registration

The core purpose of HostFixture is to provide a fluent API for configuring `IHost` instances. Existing or new services can be registered or replaced in the `IServiceCollection` using the `HostFixtureBuilder`, with any lifetime required (e.g., `Singleton`, `Scoped`, `Transient`).

Imagine we have a service which itself reaches out to an external Rest API to grab the current weather for a given location. When running integration tests, actually reaching out to this dependency may not be desirable or possible, so instead we can replace the registered service with a mock or stub implementation. The key is that our replacement satisfies the same interface as the original service, so that our code can continue to function as expected.



```csharp
using HostFixture;
```



## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact
For any questions, comments, or feedback, use the contact information listed at https://github.com/PatchworkHorse