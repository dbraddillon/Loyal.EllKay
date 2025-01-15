using Loyal.EllKay.App;
using RestSharp;

//https://github.com/dbraddillon/Loyal.EllKay Share or move to Loyal...

const string baseUrl = "https://services.lkstaging.com";
//const string baseUrl = "https://services.lkcloud.com";

const string tokenEndpoint = "https://auth2.lkidentity.com/connect/token";
//token metadata endpoint 
//https://fhir.lkstaging.com/R4/metadata?_format=json

Console.WriteLine("Let's go.....");

var config = Config.BindFromEnvironment();

var client = new RestClient();
var request = new RestRequest(tokenEndpoint, Method.Post);

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
