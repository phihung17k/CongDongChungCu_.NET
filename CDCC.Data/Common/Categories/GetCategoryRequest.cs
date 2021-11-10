using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Common.Categories
{
    public class GetCategoryRequest
    {
        //1 store id cụ thể
        [FromQuery(Name = "store-id")]
        public int? StoreId { get; set; }
    }
}
