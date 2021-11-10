using Microsoft.AspNetCore.Mvc;

namespace CDCC.Data.Common
{
    public class GetPoiPagingRequest : PagingRequest
    {
        [FromQuery(Name = "name")]
        public string Name { get; set; }
        [FromQuery(Name = "status")]
        public bool? Status { get; set; }
        [FromQuery(Name = "poitype-id")]
        public int? PoitypeId { get; set; }
        [FromQuery(Name = "apartment-id")]
        public int? ApartmentId { get; set; }
    }
}
