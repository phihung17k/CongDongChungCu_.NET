using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDCC.Bussiness.ViewModels.Building
{
    public class BuildingInsertModel
    {
        public string Name { get; set; }

        [JsonPropertyName("apartment_id")]
        public int ApartmentId { get; set; }
        public bool? Status { get; set; }
    }
}
