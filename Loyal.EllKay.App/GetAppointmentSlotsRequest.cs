using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Loyal.EllKay.App;

public class GetAppointmentSlotsRequest : RequestBase
{
    public GetAppointmentSlotsRequest()
    :base()
    {
        Request = new GetAppointmentSlots();
    }
    
    [JsonProperty("request")]
    public GetAppointmentSlots Request { get; set; }
}

public class GetAppointmentSlots
{
    [JsonProperty("startDate")]
    [Required]
    public DateTimeOffset StartDate { get; set; }//2019-01-01T00:00:00+00:00
    
    [JsonProperty("endDate")]
    [Required]
    public DateTimeOffset EndDate { get; set; }//2019-01-01T00:00:00+00:00
    
    [JsonProperty("facilityIds", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string[] FacilityIds { get; set; }
    
    [JsonProperty("physicianIds", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string[] PhysicianIds    { get; set; }   
    
    [JsonProperty("appointmentValues", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string[] AppointmentTypes { get; set; }
    
    [JsonProperty("resources", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Resource[] Resources { get; set; }
}

public class Resource
{
    public string Id { get; set; }
}