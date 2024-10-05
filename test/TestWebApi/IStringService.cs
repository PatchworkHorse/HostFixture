namespace TestWebApi;

public interface IStringService
{
    public string GenerateRandomString(int length); 

    public string ReverseString(string input);
}