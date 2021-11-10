using CDCC.Data.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDCC.Data.Common.Stores
{
    public class ViewStores
    {
        [JsonPropertyName("store_id")]
        public int StoreId { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("opening_time")]
        public DateTime OpeningTime { get; set; }

        [JsonPropertyName("closing_time")]
        public DateTime ClosingTime { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }

        //kèm theo chủ sở hữu của store
        //
        [JsonPropertyName("owner_store")]
        public string OwnerStore { get; set; }
        //
        //public int ProductCount { get; set; }
        ////
        ////                      Paging mặc định theo cho list product
        //// 1 trang hiện PageSizeProducts
        //public int PageSizeProdcuts = 5;
        //// 1 IndexPageProduct
        //public int IndexPageProduct = 1;
        //// 1 TotalPageOfProduct
        //public int TotalPageOfProduct { get; set; }

        //View 1 store sẽ kèm 1 listProduct
        //public List<ViewProducts> listProduct { get; set; }



    }
   }
