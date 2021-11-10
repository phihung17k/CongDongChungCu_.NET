using CDCC.Data.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDCC.Bussiness.ViewModels
{
    public class UpdatePostModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Status Status { get; set; }
    }
}
