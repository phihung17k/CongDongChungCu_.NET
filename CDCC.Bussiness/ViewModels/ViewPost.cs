using System;
using System.Collections.Generic;

namespace CDCC.Bussiness.ViewModels
{
    public class ViewPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ViewComment> listComment { get; set; }

    }
}
