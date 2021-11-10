using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.Resident
{
    public class ResidentViewModel
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool? Status { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("building_id")]
        public int BuildingId { get; set; }

        [JsonPropertyName("apartment_id")]
        public int ApartmentId { get; set; }
    }
}
