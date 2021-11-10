using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Poi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }
        public int PoitypeId { get; set; }
        public int ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Poitype Poitype { get; set; }
    }
}
