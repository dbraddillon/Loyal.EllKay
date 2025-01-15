namespace Loyal.EllKay.App;

public class Config
{
    public string EllKayClientId { get; set; }
    public string EllKayClientSecret { get; set; }

    public static Config BindFromEnvironment()
    {
        return new Config
        {
            EllKayClientId = Environment.GetEnvironmentVariable("EllKay_ClientId"),
            EllKayClientSecret = Environment.GetEnvironmentVariable("EllKay_ClientSecret")
        };
    }
}