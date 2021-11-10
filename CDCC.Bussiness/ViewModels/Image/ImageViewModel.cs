using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.Image
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        [JsonPropertyName("album_id")]
        public int AlbumId { get; set; }
    }
}
