namespace Services; 

public class EchoService : IEchoService
{
    public string Echo(string message)
        => message;
}