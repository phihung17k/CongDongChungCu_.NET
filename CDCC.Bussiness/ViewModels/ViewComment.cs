using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Bussiness.ViewModels
{
    public class ViewComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public int PostId { get; set; }
        public int ResidentId { get; set; }

    }
}
