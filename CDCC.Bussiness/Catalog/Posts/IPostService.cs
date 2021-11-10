using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Posts;
using CDCC.Data.ViewModels.Posts;
using System.Collections;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Posts
{
    public interface IPostService
    {
        //CREATE UPDATE DELETE COMMENT
        Task<ViewPosts> InsertPost(InsertPostModel post);

        Task<bool> UpdatePost(UpdatePostModel post);

        Task<bool> DeletePost(int postID);

        ///GET
        
        PagingResult<ViewPosts> GetAllPostBelongToApartment_Paging (GetPostPagingRequest request);

        Task<ViewPosts> GetPostById(int PostId);

    }
}
