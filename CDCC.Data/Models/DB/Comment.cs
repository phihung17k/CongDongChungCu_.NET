using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public int PostId { get; set; }
        public int ResidentId { get; set; }
        public bool? Status { get; set; }

        public virtual Post Post { get; set; }
        public virtual Resident Resident { get; set; }
    }
}
