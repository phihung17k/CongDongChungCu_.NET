using CDCC.Data.Common.Enum;
using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Store Store { get; set; }
    }
}
