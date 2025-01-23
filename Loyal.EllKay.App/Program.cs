using Loyal.EllKay.App;
using Newtonsoft.Json;
using RestSharp;

//github loc.
//https://github.com/dbraddillon/Loyal.EllKay Share or move to Loyal...

//LOYAL appointment DL service 
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
    Token? token = null;

    var client = new RestClient();
    var request = new RestRequest(tokenEndpoint, Method.Post);

    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
    request.AddParameter("grant_type", "client_credentials");
    request.AddParameter("client_id", config.EllKayClientId);
    request.AddParameter("client_secret", config.EllKayClientSecret);

    try
    {
        var response = client.Execute(request);
        if (response is { IsSuccessful: true, Content: not null }) token = JsonConvert.DeserializeObject<Token>(response.Content);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    return token;
}

void TestGetAppointmentsCall(Token token)
{
    var url = $"{baseUrl}LKAppointments/GetOpenAppointmentSlots";
    var options = new RestClientOptions(url);
    var client = new RestClient(options);
    var request = new RestRequest("");
    //request.AddHeader("Content-Type", "text/plain");
    //request.AddHeader("Accept", "*/*");
    //request.AddHeader("Accept-Encoding", "gzip, deflate, br");
    if (config.EllKaySiteServiceKey != null) request.AddHeader("SiteServiceKey", config.EllKaySiteServiceKey);
    request.AddHeader("Authorization", $"Bearer {token.AccessToken}");
    request.AddHeader("Content-Type", "application/json");
    
    GetAppointmentSlotsRequest r = new GetAppointmentSlotsRequest
    {
        Request =
        {
            StartDate = DateTimeOffset.Now,
            EndDate = DateTimeOffset.Now.AddDays(14)
        },
    };

    //var body = JsonConvert.SerializeObject(r);
    
    var body ="{\"request\":{\"startDate\":\"2025-01-23T08:37:52.093677-05:00\",\"endDate\":\"2025-02-06T08:37:52.093758-05:00\"}}";
    
    request.AddJsonBody(body, false);
    
    var response = client.Post(request);

    if (response is { IsSuccessful: true, Content: not null })
    {
        AppointmentSlot[]? slots = JsonConvert.DeserializeObject<AppointmentSlot[]>(response.Content);
    }
}