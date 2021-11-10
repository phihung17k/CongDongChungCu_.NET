using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class News
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? Status { get; set; }
        public int ApartmentId { get; set; }
        public string Title { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
