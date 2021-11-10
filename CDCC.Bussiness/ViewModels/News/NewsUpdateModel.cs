using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.News
{
    public class NewsUpdateModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
