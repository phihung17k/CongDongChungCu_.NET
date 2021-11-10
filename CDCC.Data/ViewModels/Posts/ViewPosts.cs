using CDCC.Data.Common.Enum;
using CDCC.Data.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CDCC.Data.ViewModels.Posts
{
    public class ViewPosts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        //
        public Status Status { get; set; }

        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("resident_id")]
        public int residentId { get; set; }

        //[JsonPropertyName("owner_post")]
        //public String OwnerPost { get; set; }
        //                      Paging mặc định theo cho list product
        ////// 1 trang hiện PageSizeProducts
        ////public int PageSizeComment = 5;
        ////// 1 IndexPageProduct
        ////public int IndexPageComment = 1;
        ////// 1 TotalPageOfProduct
        ////public int TotalPageOfComment { get; set; }
        ////public int CommentCount { get; set; }
        ////public List<ViewComments> listComment { get; set; }

    }
}
