using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Apartment
    {
        public Apartment()
        {
            Albums = new HashSet<Album>();
            Buildings = new HashSet<Building>();
            News = new HashSet<News>();
            Pois = new HashSet<Poi>();
            Residents = new HashSet<Resident>();
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Poi> Pois { get; set; }
        public virtual ICollection<Resident> Residents { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
