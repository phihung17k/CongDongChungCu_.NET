using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class User
    {
        public User()
        {
            Residents = new HashSet<Resident>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsSystemAdmin { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Resident> Residents { get; set; }
    }
}
