using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Store
    {
        public Store()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }
        public int ApartmentId { get; set; }
        public int ResidentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Resident Resident { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
