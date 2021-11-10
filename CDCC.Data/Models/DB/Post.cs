using CDCC.Data.Common.Enum;
using System;
using System.Collections.Generic;

#nullable disable

namespace CDCC.Data.Models.DB
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
        public int ResidentId { get; set; }

        public virtual Resident Resident { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
