using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Common.Stores
{
    public class GetStore
    {

        [FromQuery(Name = "store-id")]
        public int StoreId { get; set; }


        [FromQuery(Name = "resident-id")]
        public int ResidentId { get; set; }


    }
}
