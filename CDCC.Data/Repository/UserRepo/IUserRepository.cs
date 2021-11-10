using CDCC.Data.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Data.Repository.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckExistedUser(string email);
        Task<User> GetUserByEmail(string email);
        bool CheckSystemAdmin(int id);
        bool CheckExistedUser(int id);
        void RemoveResidentRelationship(int userId);
    }
}
