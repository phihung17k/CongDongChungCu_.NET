using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDCC.Bussiness.ViewModels
{
    public class UpdateCommentModel
    {
        [JsonPropertyName("comment_id")]
        [JsonIgnore]
        public int CommentId { get; set; }
        public string Content { get; set; }

        [JsonPropertyName("owner_comment_id")]
        public int OwnerCommentId { get; set; }
    }
}
