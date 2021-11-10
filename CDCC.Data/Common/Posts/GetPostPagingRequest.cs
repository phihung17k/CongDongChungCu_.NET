using CDCC.Data.Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace CDCC.Data.Common.Posts
{
    public class GetPostPagingRequest : PagingRequest
    {
        //Apartment id
        // 1 chung cu cụ thể
        [FromQuery(Name = "apartment-id")]
        public int ApartmentId { get; set; }

        //Post Id cụ thể
        //public int? PostId { get; set; }

        //Search title
        public string? Title { get; set; }

        //lấy status các bài post
        public Status? status { get; set; }
    }
}
