using Moq;
using TestWebApi;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

namespace HostFixtureTests;

public class ServiceCollectionTests
{
    [Theory]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void should_replace_service_collection_entries_with_instance(ServiceLifetime lifetime)
    {
        // This test verifies that service collection replacement works as expected.
        // We expect two things: 
        // 1. The service collection contain an instance of IStringService
        // 2. The resolved instance is *EXACTLY* the same as the mock object. Verify this by comparing the hash codes of the two objects

        // Arrange
        var mock = new Mock<IStringService>();
        mock.Setup(x => x.GenerateRandomString(It.IsAny<int>())).Returns("I'm a mocked service!");

        // Create a builder instance from the demo project
        var builder = Program.CreateBuilder(Array.Empty<string>());

        // Create a fixture builder instance from the demo project. The underlying HostApplicationBuilder is the source of the fixture
        var fixturedBuilder = builder.ConfigureFixture();

        // Act

        _ = lifetime switch
        {
            ServiceLifetime.Singleton => fixturedBuilder.RegisterSingleton(typeof(IStringService), mock.Object),
            ServiceLifetime.Scoped => fixturedBuilder.RegisterScoped(typeof(IStringService), mock.Object),
            ServiceLifetime.Transient => fixturedBuilder.RegisterTransient(typeof(IStringService), mock.Object),
            _ => throw new ArgumentException("No lifetime specified")
        };

        var fixturedHost = builder.Build();

        // Assert

        var service = fixturedHost.Services.GetRequiredService<IStringService>();
        Assert.NotNull(service);
        Assert.Equal(service.GetHashCode(), mock.Object.GetHashCode());
    }

    [Theory]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void should_replace_service_collection_entries_from_func_extension(ServiceLifetime lifetime)
    {
        // This test verifies that service collection replacement works as expected.
        // We expect two things: 
        // 1. The service collection contain an instance of IStringService
        // 2. The resolved instance is *EXACTLY* the same as the mock object. Verify this by comparing the hash codes of the two objects

        // Arrange
        var mock = new Mock<IStringService>();
        mock.Setup(x => x.GenerateRandomString(It.IsAny<int>())).Returns("I'm a mocked service!");

        // Create a builder instance from the demo project
        var builder = Program.CreateBuilder(Array.Empty<string>());

        // Create a fixture builder instance from the demo project. The underlying HostApplicationBuilder is the source of the fixture
        var fixturedBuilder = builder.ConfigureFixture();

        // Act

        _ = lifetime switch
        {
            ServiceLifetime.Singleton 
                => fixturedBuilder.RegisterSingleton(() => mock.Object),
            
            ServiceLifetime.Scoped 
                => fixturedBuilder.RegisterScoped(() => mock.Object),
            
            ServiceLifetime.Transient 
                => fixturedBuilder.RegisterTransient(() => mock.Object),
            
            _ => throw new ArgumentException("No lifetime specified")
        };

        var fixturedHost = builder.Build();

        // Assert
        fixturedHost.Services.GetRequiredService<IStringService>()
            .Should()
            .NotBeNull()
            .And
            .BeSameAs(mock.Object);
    }

    [Theory]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void should_replace_service_collection_entries_from_func_extension_service_provider(ServiceLifetime lifetime)
    {
        // This test verifies that service collection replacement works as expected.
        // We expect two things: 
        // 1. The service collection contain an instance of IStringService
        // 2. The resolved instance is *EXACTLY* the same as the mock object. Verify this by comparing the hash codes of the two objects

        // Arrange
        var mock = new Mock<IStringService>();
        mock.Setup(x => x.GenerateRandomString(It.IsAny<int>())).Returns("I'm a mocked service!");

        // Create a builder instance from the demo project
        var builder = Program.CreateBuilder(Array.Empty<string>());

        // Create a fixture builder instance from the demo project. The underlying HostApplicationBuilder is the source of the fixture
        var fixturedBuilder = builder.ConfigureFixture();

        // Act

        // Todo: We actually need to use the IServiceProvide to resolve or create an instance of a test service
        _ = lifetime switch
        {
            ServiceLifetime.Singleton
                => fixturedBuilder.RegisterSingleton((IServiceProvider) => mock.Object),

            ServiceLifetime.Scoped
                => fixturedBuilder.RegisterScoped((IServiceProvider) => mock.Object),

            ServiceLifetime.Transient
                => fixturedBuilder.RegisterTransient((IServiceProvider) => mock.Object),

            _ => throw new ArgumentException("No lifetime specified")
        };


        var fixturedHost = builder.Build();

        // Assert
        fixturedHost.Services.GetRequiredService<IStringService>()
            .Should()
            .NotBeNull()
            .And
            .BeSameAs(mock.Object);
    }
}