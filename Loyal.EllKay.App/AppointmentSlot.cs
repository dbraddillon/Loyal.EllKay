using Newtonsoft.Json;

namespace Loyal.EllKay.App;

public class AppointmentSlot
{
    [JsonProperty("physician_id")]
    public string PhysicianId { get; set; }

    [JsonProperty("facility_id")]
    public string FacilityId { get; set; }

    [JsonProperty("start_time")]
    public string StartTime { get; set; }

    [JsonProperty("duration")]
    public string Duration { get; set; }

    [JsonProperty("slot_id")]
    public string SlotId { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("resources")]
    public Resources[] Resources { get; set; }
}

public class Resources
{
    [JsonProperty("id")]
    public string Id { get; set; }
}

