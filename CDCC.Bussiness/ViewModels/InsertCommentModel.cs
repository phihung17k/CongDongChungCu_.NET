using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels
{
    public class InsertCommentModel
    {

        public string Content { get; set; }

        [JsonPropertyName("post_id")]
        public int PostId { get; set; }

        [JsonPropertyName("resident_id")]
        public int ResidentId { get; set; }
    }
}
