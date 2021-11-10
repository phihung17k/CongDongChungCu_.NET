using CDCC.Data.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDCC.Data.ViewModels.Products
{
    public class ViewProducts
    {
        public ViewProducts()
        { }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }


        [JsonPropertyName("category_id")]
        public int CategoryId {get; set;}

        [JsonPropertyName("category_name")]
        //thuộc category nào
        public string CategoryName { get; set; }

        [JsonPropertyName("store_id")]
        public int StoreId { get; set; }

        [JsonPropertyName("name_store")]
        public String NameStore { get; set; }

    }
}
