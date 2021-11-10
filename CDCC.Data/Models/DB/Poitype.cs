using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Poitype
    {
        public Poitype()
        {
            Pois = new HashSet<Poi>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Poi> Pois { get; set; }
    }
}
