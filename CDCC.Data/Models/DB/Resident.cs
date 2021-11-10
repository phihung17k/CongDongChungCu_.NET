using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Resident
    {
        public Resident()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool? Status { get; set; }
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public int ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Building Building { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
