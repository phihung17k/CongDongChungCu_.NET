using CDCC.Data.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDCC.Bussiness.ViewModels
{
    public class UpdateProductModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }

    }
}
