using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Album
    {
        public Album()
        {
            Images = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid? ExternalCode { get; set; }
        public int OwnerId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
