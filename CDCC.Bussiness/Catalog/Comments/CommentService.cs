using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Comments;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using CDCC.Data.Repository.CommentRepository;
using CDCC.Data.ViewModels.Comments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Comments
{
    public class CommentService : ICommentService
    {

        //goi IGenericRepository
        ICommentRepository _genericRepositoryComment;
        CongDongChungCuContext _context;
        public CommentService(ICommentRepository genericRepositoryComment, CongDongChungCuContext context)
        {
            _genericRepositoryComment = genericRepositoryComment;
            //
            _context = context;
        }


        //DELETE COMMENT
        public async Task<bool> DeleteComment(int commentID, int OwnerCommentId)
        {
            try
            {
                //
                Boolean check = false;
                //get entity
                Comment cm = await _genericRepositoryComment.Get_By_Id(commentID);
                //
                if (cm != null)
                {
                    //check xem cái người đó có sửa đúng ngay cái comment của họ kh
                    if (cm.ResidentId == OwnerCommentId)
                    {
                        //khi đụng tới await thì trả ra kết quả của Task
                        check = await _genericRepositoryComment.Delete_(cm);
                        //
                        return check;
                    }
                }
                //
                return check;
            }catch(Exception )
            {
                throw;
            }
        }

        //Get Comment By Id
        public async Task<ViewComments> GetCommentById(int id)
        {

            try
            {
                //
                Comment comment = await _genericRepositoryComment.Get_By_Id(id);
                //
                ViewComments comView = null;
                //
                if (comment != null)
                {
                    comView = new ViewComments
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        CreatedTime = comment.CreatedTime,
                        PostId = comment.PostId,
                        ResidentId = comment.ResidentId,
                        OwnerNameComment = comment.Resident.User.Fullname

                    };
                }
                return comView;
            }
            catch (Exception) { throw; }
        }
    

        //Get Comment của post theo post id
        public PagingResult<ViewComments> GetCommentOfPost(GetCommentPagingRequest request)
        {
            try {
                PagingResult<ViewComments> kq = _genericRepositoryComment.GetCommentOfPost(request);
                return kq;
            }
            catch(Exception )
            {
                throw;
            }
        }

        //INSERT COMMENT
        public async Task<ViewComments> InsertComment(InsertCommentModel comment)
        {
            try
            {
                ViewComments vc = new ViewComments();
                //
                Comment cm = new Comment();
                //Content
                cm.Content = comment.Content;
                //Create time
                cm.CreatedTime = DateTime.Now;
                //Post ID
                cm.PostId = comment.PostId;
                //Resident ID
                cm.ResidentId = comment.ResidentId;
                //
                int result = await _genericRepositoryComment.Insert_(cm);
                //
                //RETURN DATA
                if (result > 0)
                {
                    //check = await _genericRepositoryComment.Insert_(cm);
                    //set model view
                    Comment getComment = await _genericRepositoryComment.Get_By_Id(result);


                    // gọi load lazy
                    var test = _context.Entry(getComment);
                    //
                    test.Reference((Comment cment) => cment.Resident).Load();
                    //

                    var userId = getComment.Resident.UserId;

                    //
                    var FullName = from User us in _context.Users
                                   where us.Id == userId
                                   select us.Fullname;

                    //
                    vc.Id = getComment.Id;
                    vc.Content = getComment.Content;
                    vc.CreatedTime = getComment.CreatedTime;
                    vc.PostId = getComment.PostId;
                    vc.ResidentId = getComment.ResidentId;
                    vc.OwnerNameComment = FullName.ToList()[0];
                    //
                    return vc;
                }

                return vc;
            }catch(Exception)
            {
                throw;
            }
        }

        //UPDATE COMMENT
        public async Task<bool> UpdateComment(UpdateCommentModel comment)
        {
            try
            {
                //
                Boolean check = false;
                //
                Comment cm = await _genericRepositoryComment.Get_By_Id(comment.CommentId);
                //
                if (cm != null)
                {
                    //check xem cái người đó có sửa đúng ngay cái comment của họ kh
                    if (cm.ResidentId == comment.OwnerCommentId)
                    {
                        //set content 
                        cm.Content = (!comment.Content.Equals("")) ? comment.Content : cm.Content;
                        //
                        check = await _genericRepositoryComment.Update_(cm);
                        //
                        return check;
                    }
                }
                return check;
            }catch(Exception)
            {
                throw;
            }
        }
    }
}
