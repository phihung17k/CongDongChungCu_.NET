using CDCC.Data.Common.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Common.Products
{
    public class GetProductPagingRequest : PagingRequest
    {

        //Phần Product Filter theo listore
        //1. nếu kh có filter product thì trả về list product
        //
        //1 store id cụ thể
        [FromQuery(Name = "store-id")]
        public int? StoreId { get; set; }

        //Status
        public Status? Status { get; set; }

        //Ten Product
        [FromQuery(Name = "name-of-product")]
        public string? NameOfProduct { get; set; }

        //Loai Product
        [FromQuery(Name = "category-name")]
        public string? CategoryName { get; set; }

        //Loai Product
        [FromQuery(Name = "category-id")]
        public int? CategoryId { get; set; }

        //Giá tiền từ
        [FromQuery(Name = "price-from")]
        public double? PriceFrom { get; set; }

        //Giá tiền đến
        [FromQuery(Name = "price-to")]
        public double? PriceTo { get; set; }




    }
}
