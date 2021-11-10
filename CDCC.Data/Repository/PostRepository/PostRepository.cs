using CDCC.Data.Common;
using CDCC.Data.Common.Enum;
using CDCC.Data.Common.Posts;
using CDCC.Data.Models.DB;
using CDCC.Data.ViewModels.Comments;
using CDCC.Data.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDCC.Data.Repository.PostRepository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(CongDongChungCuContext context) : base(context)
        {

        }

        public int loadLazing(Post post)
        {
            // gọi load lazy
            var test = context.Entry(post);
            //
            test.Reference((Post p) => p.Resident).Load();
            //
            int residentId = post.Resident.Id;
            //
            //var FullName = from User us in context.Users
            // where us.Id == userId
            //select us.Fullname;
            //string OwnerPost = FullName.ToList()[0];

            return residentId;
        }


        public PagingResult<ViewPosts> GetPostOfApartment(GetPostPagingRequest request)
        {

            //List View Post
            List<ViewPosts> listView_post = new List<ViewPosts>();
            //ở đây ta lấy được Tổng số bài post có trong Apartment
            int Total_Post_Apartment = 0;

            //getList resident Id of Apartment do post kh có kết nói apartment id
            var List_Resident_Apartment = context.Residents.Where((Resident rs) => rs.ApartmentId == request.ApartmentId);

            //lọc tìm resident nào có post
            var List_Resident_Apartment_Post = List_Resident_Apartment.Where((Resident res) => res.Posts != null);

            // ==> ra được resident có post thuộc apartment id cụ thể

            List<Resident> List_Resident = List_Resident_Apartment_Post.ToList();

            List_Resident.ForEach((Resident rs) =>
            {

                //1 resident sẽ có nhiều post
                List<Post> list_Post = rs.Posts.ToList();


                //Trong list Post sẽ có những bài post đc Rejected, NotApproved, Approved, InActive
                //Rejected
                if (request.status == Status.Rejected)
                {
                    list_Post = list_Post.Where((Post p) => p.Status == Status.Rejected).ToList();
                }

                //NotApproved
                if (request.status == Status.NotApproved)
                {
                    list_Post = list_Post.Where((Post p) => p.Status == Status.NotApproved).ToList();
                }

                //Approved
                if (request.status == Status.Approved)
                {
                    list_Post = list_Post.Where((Post p) => p.Status == Status.Approved).ToList();
                }

                //Inactive
                if (request.status == Status.InActive)
                {
                    list_Post = list_Post.Where((Post p) => p.Status == Status.InActive).ToList();
                }

                //Thống kê ra tổng số bài post
                if (list_Post.Count != 0)
                {
                    Total_Post_Apartment = Total_Post_Apartment + list_Post.Count;
                }

                //Set model ViewPosts
                list_Post.ToList().ForEach((Post p) =>
                {
                    //ViewPost
                    ViewPosts vp = new ViewPosts();
                    //Set ViewPost
                    vp.Id = p.Id;
                    vp.Title = p.Title;
                    vp.Content = p.Content;
                    vp.CreatedDate = p.CreatedDate;
                    vp.residentId = p.Resident.Id;
                    vp.Status = p.Status;
                    listView_post.Add(vp);
                });// end of duyệt post


            }
            );//end of duyệt resident

            //filter cái post title
            if (request.Title != null)
            {
                //
                listView_post = (from ViewPosts eop in listView_post
                                 where eop.Title.Contains(request.Title)
                                 select eop).ToList();
                //
                Total_Post_Apartment = listView_post.Count;
                //
                listView_post = listView_post.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();
            }
            else
            {

                //do kh có khóa fk từ post id sang apartment id
                //==> ra ta có được list Post thuộc 1 apartment 
                // ==> Paging bài post
                listView_post = listView_post.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();

            }
            //} end else

            PagingResult<ViewPosts> kq = new PagingResult<ViewPosts>(listView_post, Total_Post_Apartment, request.currentPage, request.pageSize);

            return kq;
        }
    }
}
