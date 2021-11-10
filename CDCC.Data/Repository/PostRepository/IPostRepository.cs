using CDCC.Data.Common;
using CDCC.Data.Common.Posts;
using CDCC.Data.Models.DB;
using CDCC.Data.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Repository.PostRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        //get post belong to apartment
        PagingResult<ViewPosts> GetPostOfApartment(GetPostPagingRequest request);

        //
        public int loadLazing(Post post);
    }

}
