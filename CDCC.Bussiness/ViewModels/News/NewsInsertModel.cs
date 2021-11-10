using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.News
{
    public class NewsInsertModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public int ApartmentId { get; set; }
    }
}
