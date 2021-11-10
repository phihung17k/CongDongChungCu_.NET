using System;
using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels
{
    public class InsertStoreModel
    {
        public string Name { get; set; }

        [JsonPropertyName("opening_time")]
        public DateTime OpeningTime { get; set; }

        [JsonPropertyName("closing_time")]
        public DateTime ClosingTime { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }

        [JsonPropertyName("apartment_id")]
        public int ApartmentId { get; set; }

        [JsonPropertyName("resident_id")]
        public int ResidentId { get; set; } 
    }
}
