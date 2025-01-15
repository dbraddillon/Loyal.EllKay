using Loyal.EllKay.App;
using RestSharp;

const string baseUrl = "https://services.lkstaging.com";
//const string baseUrl = "https://services.lkcloud.com";
const string tokenEndpoint = "https://auth2.lkidentity.com/connect/token";

Console.WriteLine("Let's go.....");

var config = Config.BindFromEnvironment();

var client = new RestClient(tokenEndpoint);
var request = new RestRequest(baseUrl, Method.Post);

request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
request.AddParameter("grant_type", "client_credentials");
request.AddParameter("client_id", config.EllKayClientId);
request.AddParameter("client_secret", config.EllKayClientSecret);

try
{
    var response = client.Execute(request);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

Console.WriteLine("Done!");
