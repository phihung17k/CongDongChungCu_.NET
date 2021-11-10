using CDCC.Data.Common;
using CDCC.Data.Common.Comments;
using CDCC.Data.Models.DB;
using CDCC.Data.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Repository.CommentRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        PagingResult<ViewComments> GetCommentOfPost(GetCommentPagingRequest request);
    }
}
