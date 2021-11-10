using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Enum;
using CDCC.Data.Common.Posts;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository.PostRepository;
using CDCC.Data.ViewModels.Posts;
using System;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Posts
{
    public class PostService : IPostService
    {

        //goi IGenericRepository
        IPostRepository _genericRepositoryPost;


        public PostService(IPostRepository genericRepositoryPost)
        {
            //POST
            _genericRepositoryPost = genericRepositoryPost;

        }




        //GetAllPostBelongToApartment_Paging
        public PagingResult<ViewPosts> GetAllPostBelongToApartment_Paging(GetPostPagingRequest request)
        {
            try
            {
                return _genericRepositoryPost.GetPostOfApartment(request);
            }
            catch (Exception)
            {
                throw;
            }

        }

        //GET POST ID
        public async Task<ViewPosts> GetPostById(int PostId)
        {
            try
            {

                Post p = await _genericRepositoryPost.Get_By_Id(PostId);
                // Set model View
                if (p != null)
                {
                    ViewPosts vp = new ViewPosts();
                    //
                    vp.Id = p.Id;
                    vp.Title = p.Title;
                    vp.Content = p.Content;
                    vp.CreatedDate = p.CreatedDate;
                    vp.residentId = p.Resident.Id;
                    vp.Status = p.Status;
                    //
                    return vp;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //INSERT
        public async Task<ViewPosts> InsertPost(InsertPostModel post)
        {
            try
            {
                //
                Boolean check = false;
                //
                Post po = new Post();
                //set
                po.Title = post.Title;
                //content
                po.Content = post.Content;
                //create date
                po.CreatedDate = DateTime.Now;
                //resident id
                po.ResidentId = post.ResidentId;
                //status mặc định chưa duyệt
                po.Status = Status.NotApproved;
                //postId
                int result = await _genericRepositoryPost.Insert_(po);

                //                                  Return Data When Insert
                if (result > 0)
                {
                    Post p = await _genericRepositoryPost.Get_By_Id(result);
                    // Set model View
                    ViewPosts vp = new ViewPosts();
                    //
                    vp.Id = p.Id;
                    vp.Title = p.Title;
                    vp.Content = p.Content;
                    vp.CreatedDate = p.CreatedDate;
                    vp.residentId = _genericRepositoryPost.loadLazing(p);
                    vp.Status = p.Status;
                    //
                    return vp;
                }

                return null;
            }
            catch (Exception) { throw; }
        }

        //UPDATE
        public async Task<bool> UpdatePost(UpdatePostModel post)
        {
            try
            {
                //
                Boolean check = false;
                //
                Post po = await _genericRepositoryPost.Get_By_Id(post.Id);
                //
                if (po != null)
                {
                    po.Title = (!post.Title.Equals("")) ? post.Title : po.Title;
                    po.Content = (!post.Content.Equals("")) ? post.Content : po.Content;
                    po.Status = post.Status;
                    //

                    check = await _genericRepositoryPost.Update_(po);

                    return check;
                }

                return check;
            }
            catch (Exception) { throw; }
        }

        //DELETE
        public async Task<bool> DeletePost(int postID)
        {
            try
            {

                //
                Boolean check = false;
                //get entity
                Post po = await _genericRepositoryPost.Get_By_Id(postID);
                //
                if (po != null)
                {
                    //khi đụng tới await thì trả ra kết quả của Task
                    check = await _genericRepositoryPost.Delete_(po);
                    //
                    return check;
                }
                //
                return check;
            }
            catch (Exception) { throw; }
        }

        //public async Task<Hashtable> GetPost_All_BelongToApartMent(int ResidentId)
        //{
        //    //dựa vào id resident này -> ApartmentID -> toàn bộ người thuộc Apartment ID này -> những bài POST thuộc các list người này -> có Comment, Post
        //    //
        //    Hashtable userAndPost = new Hashtable();

        //    //1. tìm Apartment ID dựa vào resident ID
        //    var resident = await _genericRepositoryResident.Get_By_Id(ResidentId);

        //    // đã có được apartment id
        //    int apartmentId = resident.ApartmentId;

        //    //2. Tìm tất cả ID những người thuộc Apartment ID này 
        //    var allResident = await _genericRepositoryResident.Get_All();

        //    var listResidentID = from Resident people in allResident
        //                               where people.ApartmentId == apartmentId
        //                               select people.Id;



        //    //3.Tìm những bài Post thuộc những list id người dùng này 
        //    //3.1 lấy tất cả post
        //    List<Post> listPost_All = await _genericRepositoryPost.Get_All();

        //    //3.2 lọc post thuộc về resident id này
        //    listResidentID.ToList().ForEach(id => {

        //        //này là cứ 1 id sẽ có 1 list viewpost
        //        List<ViewPost> listViewPosts = new List<ViewPost>();


        //        //trả ra list post theo residentID
        //        var listPost_Resident = from post in listPost_All
        //                                where post.ResidentId == id
        //                                select post;


        //        //có những Resident kh có post bài
        //        List<Post> listPost_Resident_Result = listPost_Resident.ToList();
        //        if (listPost_Resident_Result.Count != 0)
        //        {
        //            //dùng navigation để lấy list comment trong 1 post đó 
        //            listPost_Resident_Result.ForEach(post =>
        //            {
        //                //
        //                ViewPost vp = new ViewPost();
        //                vp.Id = post.Id;
        //                vp.Title = post.Title;
        //                vp.CreatedDate = post.CreatedDate;

        //                //List ViewComment
        //                //Navigation
        //                List<ViewComment> vc = new List<ViewComment>();
        //                var list_Comment = post.Comments;
        //                //
        //                list_Comment.ToList().ForEach(cm =>
        //                {
        //                    ViewComment comment = new ViewComment();
        //                    comment.Id = cm.Id;
        //                    comment.Content = cm.Content;
        //                    comment.CreatedTime = cm.CreatedTime;
        //                    comment.PostId = cm.PostId;
        //                    comment.ResidentId = cm.ResidentId;
        //                    vc.Add(comment);
        //                });
        //                vp.listComment = vc;
        //                //

        //                vp.Content = post.Content;
        //                //
        //                listViewPosts.Add(vp);
        //            });

        //            //add vào 1 id đi kèm theo list viewpost
        //            userAndPost.Add(id, listViewPosts);

        //        } //end if (listPost_Resident != null)

        //    }//end foreach resident id

        //    );

        //    //sau đây mình đã có được tất cả bài post trong 1 apartment cụ thể
        //    return userAndPost;

        //}
    }
}
