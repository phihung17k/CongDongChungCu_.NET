
using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels
{
    public class InsertPostModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        //inserPost thì sẽ mặc định là chưa duyệt
        //protected Status Status { get; set; } = Status.NotApproved;

        [JsonPropertyName("resident_id")]
        public int ResidentId { get; set; }
    }
}
