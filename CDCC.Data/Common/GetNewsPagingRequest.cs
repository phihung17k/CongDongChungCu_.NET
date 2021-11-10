using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;

namespace CDCC.Data.Common
{
    public class GetNewsPagingRequest : PagingRequest
    {
        [FromQuery(Name = "keyword")]
        public string Keyword { get; set; }
        [FromQuery(Name = "from-date")]
        public DateTime? FromDate { get; set; }
        [FromQuery(Name = "to-date")]
        public DateTime? ToDate { get; set; }
        [FromQuery(Name = "status")]
        public bool? Status { get; set; }
        [FromQuery(Name = "apartment-id")]
        public int? ApartmentId { get; set; }
    }
}
