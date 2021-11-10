using Microsoft.AspNetCore.Mvc;

namespace CDCC.Data.Common.Stores
{
   public class GetStorePagingRequest : PagingRequest
    {


        //kế thừa thuộc tính paging của request nữa
        //tất cả trường này có thể null

        // 1 chung cu cụ thể
        [FromQuery(Name = "apartment-id")]
        public int ApartmentId { get; set; }
        // Name Of Store
        [FromQuery(Name = "name-of-store")]
        public string? NameOfStore { get; set; }

        //1 lấy 1 store cụ thể trong apartment
        //public int? StoreId { get; set; }
        //Phần Product Filter theo listore
        //1. nếu kh có filter product thì trả về list product
        //

        //                                                       PRODUCT
        ////Ten Product
        //public string? NameOfProduct { get; set; }

        ////Loai Product
        //public string? CategoryName { get; set; }

        ////Giá tiền từ
        //public int? PriceFrom { get; set; }

        ////Giá tiền đến
        //public int? PriceTo { get; set; }

    }
}
