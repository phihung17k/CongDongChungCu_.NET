using CDCC.Data.Common.Enum;
using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels
{
    public class InsertProductModel
    {

        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        //public Status Status { get; set; }

        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }

        [JsonPropertyName("store_id")]
        public int StoreId { get; set; }


    }
}
