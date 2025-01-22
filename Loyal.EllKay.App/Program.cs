using Loyal.EllKay.App;
using Newtonsoft.Json;
using RestSharp;

//https://github.com/dbraddillon/Loyal.EllKay Share or move to Loyal...

//appointment DL service 
//https://github.com/loyalhealth/EHRApi/blob/c121855f596d9b390179804b49e5d0b9c0a88498/IntegrationTests/EHRApi.Appointment.WebApi.IntegrationTests/Controllers/AppointmentControllerTests.cs#L33

const string baseUrl = "https://services.lkstaging.com/";
//const string baseUrl = "https://services.lkcloud.com/";

const string tokenEndpoint = "https://auth2.lkidentity.com/connect/token";
//token metadata endpoint 
//https://fhir.lkstaging.com/R4/metadata?_format=json

Console.WriteLine("Let's go.....");

var config = Config.BindFromEnvironment();

var token = GetToken();

if (token != null) TestGetAppointmentsCall(token);
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
        if (response.IsSuccessful) token = JsonConvert.DeserializeObject<Token>(response.Content);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    return token;
}

void TestGetAppointmentsCall(Token token)
{
    var options = new RestClientOptions($"{baseUrl}LKAppointments/GetOpenAppointmentSlots");
    var client = new RestClient(options);
    var request = new RestRequest("");
    request.AddHeader("accept", "application/json");
    request.AddHeader("siteServiceKey", config.EllKaySiteServiceKey);
    request.AddHeader("Authorization", $"Bearer {token.AccessToken}");
    
    GetAppointmentSlotsRequest r = new GetAppointmentSlotsRequest
    {
        Authentication = 
        {
            SiteServiceKey = config.EllKaySiteServiceKey,
            SubscriberKey = "f51eeba4-4cf9-4dad-8d00-3b54aee6e5a8"
        },
        Request =
        {
            StartDate = DateTimeOffset.Now,
            EndDate = DateTimeOffset.Now.AddDays(14)
        },
    };

    //var body = JsonConvert.SerializeObject(r);
    //request.AddJsonBody(body, false);
    request.AddJsonBody("{\"authentication\":{\"siteServiceKey\":\"4dfd870d-2193-4414-9992-97d65e5d1a10\"},\"request\":{\"startDate\":\"2025-01-25T00:00:00+00:00\",\"endDate\":\"2025-02-17T00:00:00+00:00\"}}", false);
    
    var response = client.Post(request);

    if (response.IsSuccessful)
    {
        AppointmentSlot[] slots = JsonConvert.DeserializeObject<AppointmentSlot[]>(response.Content);
    }
}