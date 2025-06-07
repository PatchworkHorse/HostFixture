using System.Text;
using Microsoft.Extensions.Options;
using TestWebApi.Config;

namespace TestWebApi.Services;

public class StringService : IStringService
{
    public StringService(IOptions<ApiConfig> config)
        => Config = config?.Value ?? throw new ArgumentNullException(nameof(config));


    public ApiConfig Config { get; }
    public string GenerateRandomString(int length)
    {
        var random = new Random();
        var result = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            result.Append((char)random.Next(65, 90));
        }
        return result.ToString();
    }
    public string ReverseString(string input)
    {
        var result = new StringBuilder();
        for (int i = input.Length - 1; i >= 0; i--)
        {
            result.Append(input[i]);
        }
        return result.ToString();
    }
    
    public string ReturnFromConfig()
        => Config.StringToReturn;
}