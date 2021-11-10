using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Common.Comments
{
   public class GetCommentPagingRequest : PagingRequest
    {
        //Post Id
        [FromQuery(Name = "post-id")]
        public int PostId { set; get; }
    }
}
