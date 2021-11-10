using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.Poi
{
    public class PoiUpdateModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        [JsonPropertyName("poitype_id")]
        public int PoitypeId { get; set; }
    }
}
