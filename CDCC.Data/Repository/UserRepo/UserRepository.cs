using CDCC.Data.Models.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCC.Data.Repository.UserRepo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CongDongChungCuContext context) : base(context)
        {
        }

        public async Task<bool> CheckExistedUser(string email)
        {
            var query = from u in context.Users
                        select u;

            query = query.Where(x => x.Email == email);
            if(query.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckExistedUser(int id)
        {
            var query = from u in context.Users
                        where u.Id == id
                        select u.Id;
            if(query.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckSystemAdmin(int id)
        {
            var query = from u in context.Users
                        where u.Id == id
                        select u.IsSystemAdmin;
            return query.First();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var query = from u in context.Users
                        where u.Email == email
                        select u;
            User user = query.First();
            return user;
        }

        public void RemoveResidentRelationship(int userId)
        {
            //List<Resident> list = new List<Resident>();
            var query = from u in context.Users
                        select u;
            //query = query.Where(r => r.UserId == userId);
            //list = query.ToList();
            //if (list.Count > 0)
            //{
            //    //context.Residents.Where(r => r.UserId == userId).ToList();
            //}
            query = query.Where(u => u.Id == userId);
            if(query.Count() > 0)
            {
                query.First().Residents.Clear();
                context.SaveChanges();
            }
            
        }
    }
}
