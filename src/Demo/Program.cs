using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services; 
using System.CommandLine; 


public class Program
{
    public static async Task Main(string[] args)
    {
        IHost host = CreateHostBuilder(args).Build();

        RootCommand rootCommand = 
            new RootCommand("Demo commands for HostFixture"); 

        // Echo 
        Command echoCommand = 
            new Command("echo", "Echoes the input string"); 

        echoCommand.AddAlias("e"); 
        echoCommand.AddArgument(new Argument<string>("input", "The input string to echo"));
        // HTTP
        Command httpCommands 
            = new Command("http", "HTTP commands"); 
        
        httpCommands.AddAlias("h");

        // Construct the command tree, invoke the command
        rootCommand.AddCommand(echoCommand); 
        rootCommand.AddCommand(httpCommands);

        await rootCommand.InvokeAsync(args); 
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddTransient<IEchoService, EchoService>();
        });       
}