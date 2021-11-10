using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Building
    {
        public Building()
        {
            Residents = new HashSet<Resident>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ApartmentId { get; set; }
        public bool? Status { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual ICollection<Resident> Residents { get; set; }
    }
}
