using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.Poi
{
    public class PoiInsertModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [JsonPropertyName("poitype_id")]
        public int PoitypeId { get; set; }
        [JsonPropertyName("apartment_id")]
        [JsonIgnore]
        public int ApartmentId { get; set; }
    }
}
