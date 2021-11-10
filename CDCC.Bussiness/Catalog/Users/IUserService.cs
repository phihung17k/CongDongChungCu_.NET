using CDCC.Bussiness.ViewModels.User;
using CDCC.Data.Common;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Users
{
    public interface IUserService
    {
        PagingResult<UserViewModel> GetAll(PagingRequest request);
        Task<UserViewModel> Get(int id);
        Task<UserViewModel> Insert(UserInsertModel user);
        Task<bool> Update(UserViewModel user);
        Task<bool> UpdateStatus(int id);
        Task<bool> Delete(int id);
        Task<UserViewModel> GetUserByEmail(string email);
        bool CheckSystemAdmin(int id);
        bool CheckExistedUser(int id);
    }
}
