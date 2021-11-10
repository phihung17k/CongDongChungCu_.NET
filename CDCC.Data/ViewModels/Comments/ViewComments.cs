using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDCC.Data.ViewModels.Comments
{
    public class ViewComments
    {
        
        public int Id { get; set; }
        public string Content { get; set; }

        [JsonPropertyName("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonPropertyName("post_id")]
        public int PostId { get; set; }

        [JsonPropertyName("resident_id")]
        public int ResidentId { get; set; }

        //ai la nguoi comment
        [JsonPropertyName("owner_name_comment")]
        public String OwnerNameComment { get; set; }
    }
}
