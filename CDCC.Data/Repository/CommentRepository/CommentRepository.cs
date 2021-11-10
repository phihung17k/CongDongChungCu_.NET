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
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(CongDongChungCuContext context) : base(context)
        {

        }

        public PagingResult<ViewComments> GetCommentOfPost(GetCommentPagingRequest request)
        {
            //ListView_Comment
            List<ViewComments> listView_Comment = new List<ViewComments>();
            //
            int CountComment = 0;
            //IF HAS POST ID
            if (request.PostId != 0)
            {
                var Post = context.Posts.Where(p => p.Id == request.PostId);
                Post.ToList().ForEach(p =>
                {

                    //List_Comment
                    List<Comment> list_Comment = p.Comments.OrderByDescending(x => x.CreatedTime).ToList();

                    CountComment = list_Comment.Count;

                    //paging comment
                    list_Comment = list_Comment.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();

                    //1 comment sẽ có thông tin
                    list_Comment.ForEach((Comment cm) =>
                    {

                        //lưu vào Viewcomment
                        ViewComments vc = new ViewComments();
                        //
                        vc.Id = cm.Id;
                        vc.Content = cm.Content;
                        vc.CreatedTime = cm.CreatedTime;
                        vc.ResidentId = cm.ResidentId;
                        vc.OwnerNameComment = cm.Resident.User.Fullname;
                        vc.PostId = cm.PostId;

                        listView_Comment.Add(vc);
                    });// end of duyệt comment

                });  
            }
            PagingResult<ViewComments> result = new PagingResult<ViewComments>(listView_Comment, CountComment, request.currentPage, request.pageSize);
            return result;
        }
    }
}
