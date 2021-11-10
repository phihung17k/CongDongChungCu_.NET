using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.Image
{
    public class ImageUpdateModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
