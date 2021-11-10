using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.Album
{
    public class AlbumUpdateModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
    }
}
