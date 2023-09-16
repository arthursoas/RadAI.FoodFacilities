using System.Text.Json.Serialization;

namespace RadAI.FoodFacilities.DTOs.Entities
{
    public class Permit
    {
        [JsonPropertyName("objectid")]
        public string Id { get; set; }

        public string? Applicant { get; set; }

        public string? FacilityType { get; set; }

        public string? CNN { get; set; }

        public string? LocationDescription { get; set; }

        public string? Address { get; set; }

        public string? Status { get; set; }

        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double Latitude { get; set; }

        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double Longitude { get; set; }
    }
}
