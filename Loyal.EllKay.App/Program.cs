using Loyal.EllKay.App;
using Newtonsoft.Json;
using RestSharp;

//https://github.com/dbraddillon/Loyal.EllKay Share or move to Loyal...

//appointment DL service 
//https://github.com/loyalhealth/EHRApi/blob/c121855f596d9b390179804b49e5d0b9c0a88498/IntegrationTests/EHRApi.Appointment.WebApi.IntegrationTests/Controllers/AppointmentControllerTests.cs#L33

const string baseUrl = "https://services.lkstaging.com";
//const string baseUrl = "https://services.lkcloud.com";

const string tokenEndpoint = "https://auth2.lkidentity.com/connect/token";
//token metadata endpoint 
//https://fhir.lkstaging.com/R4/metadata?_format=json

Console.WriteLine("Let's go.....");

var config = Config.BindFromEnvironment();

var token = GetToken();

if(token != null)
{
    TestGetAppointmentCall(token);
}
Console.WriteLine("Done!"); 


Token? GetToken()
{
    Token token = null;
    
    var client = new RestClient();
    var request = new RestRequest(tokenEndpoint, Method.Post);

    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
    request.AddParameter("grant_type", "client_credentials");
    request.AddParameter("client_id", config.EllKayClientId);
    request.AddParameter("client_secret", config.EllKayClientSecret);

    try
    {
        var response = client.Execute(request);
        if (response.IsSuccessful)
        {
            token = JsonConvert.DeserializeObject<Token>(response.Content);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    return token;
}

void TestGetAppointmentCall(Token token)
{
    var client = new RestClient(baseUrl);
    var request = new RestRequest("LKAppointments/GetOpenAppointmentSlots",Method.Get);
    request.AddHeader("Content-Type", "application/json");
    request.AddHeader("SiteServiceKey", "{site-service-key}");//get?
    request.AddHeader("Authorization", $"Bearer {token.AccessToken}");
    var response = client.Execute(request);

    if (response.IsSuccessful)
    {
        AppointmentSlot[] slots = JsonConvert.DeserializeObject<AppointmentSlot[]>(response.Content);
    }
}
