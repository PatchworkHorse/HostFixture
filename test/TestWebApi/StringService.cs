using System.Text;

namespace TestWebApi; 

public class StringService : IStringService
{
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
}