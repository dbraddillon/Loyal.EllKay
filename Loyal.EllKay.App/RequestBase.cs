using Newtonsoft.Json;

namespace Loyal.EllKay.App;

public class RequestBase
{
    public RequestBase()
    {
        Authentication = new Authentication();
    }
    
    [JsonProperty("authentication")]
    public Authentication Authentication { get; set; }
}

public class Authentication
{
    [JsonProperty("subscriberKey")]
    public string SubscriberKey { get; set; } = "LoyalHealth";
    
    [JsonProperty("siteServiceKey")]
    public string SiteServiceKey { get; set; }
}
