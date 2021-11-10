using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDCC.Bussiness.ViewModels
{
     public class UpdateStoreModel
    {
        //update liên quan đến Store
        [JsonPropertyName("store_id")]
        [JsonIgnore]
        public int StoreId { get; set; }
        public string? Name { get; set; }

        [JsonPropertyName("opening_time")]
        public DateTime? OpeningTime { get; set; }

        [JsonPropertyName("closing_time")]
        public DateTime? ClosingTime { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool Status { get; set; } = true;

    }
}
