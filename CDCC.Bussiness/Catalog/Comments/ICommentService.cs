using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Comments;
using CDCC.Data.ViewModels.Comments;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Comments
{
    public interface ICommentService
    {

        //CREATE UPDATE DELETE COMMENT
        Task<ViewComments> InsertComment(InsertCommentModel comment);

        Task<bool> UpdateComment(UpdateCommentModel comment);

        Task<bool> DeleteComment(int commentID , int OwnerCommentId);


        //GET COMMENT
        PagingResult<ViewComments> GetCommentOfPost(GetCommentPagingRequest request);

        Task<ViewComments> GetCommentById(int id);

    }
}
