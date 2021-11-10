using System;
using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.Album
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("external_code")]
        public Guid? ExternalCode { get; set; }
        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        [JsonPropertyName("apartment_id")]
        public int ApartmentId { get; set; }
    }
}
