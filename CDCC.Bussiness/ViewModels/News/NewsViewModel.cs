using System;
using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.News
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        [JsonPropertyName("apartment_id")]
        public int ApartmentId { get; set; }
    }
}
