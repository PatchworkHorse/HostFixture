namespace TestWebApi.Config;

public class ApiConfig
{
    public string StringToReturn { get; set; } = "I am the default string";

    public string ClientID { get; set; } = "";

    public string ClientSecret { get; set; } = "";

    public int MaxStringLength { get; set; } = 100;

    public bool EnableFeatureX { get; set; } = false;
}