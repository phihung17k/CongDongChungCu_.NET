using CDCC.Bussiness.ViewModels.User;
using CDCC.Data.Common;
using CDCC.Data.CustomException;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository.UserRepo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Users
{
    public class UserService : IUserService
    {
        private IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public PagingResult<UserViewModel> GetAll(PagingRequest request)
        {
            try
            {
                List<UserViewModel> userViews = new List<UserViewModel>();
                PagingResult<User> users = repository.GetAllPaging(request);
                foreach (User temp in users.items)
                {
                    userViews.Add(new UserViewModel()
                    {
                        Id = temp.Id,
                        Username = temp.Username,
                        Password = temp.Password,
                        Fullname = temp.Fullname,
                        Phone = temp.Phone,
                        Email = temp.Email,
                        IsSystemAdmin = temp.IsSystemAdmin,
                        Status = temp.Status
                    });
                }
                return new PagingResult<UserViewModel>(userViews, users.TotalCount, users.CurrentPage, users.PageSize);
            } 
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserViewModel> Get(int id)
        {
            User user = await repository.Get_By_Id(id);
            if (user == null)
            {
                throw new UserIDNotFoundException("Not found user by this id");
            }
            else if (user.Status == false)
            {
                throw new UserDeletedException("The user is banned");
            }
            return new UserViewModel()
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Fullname = user.Fullname,
                Phone = user.Phone,
                Email = user.Email,
                IsSystemAdmin = user.IsSystemAdmin,
                Status = user.Status
            };
        }

        public async Task<UserViewModel> Insert(UserInsertModel user)
        {
            User temp = new User()
            {
                Username = user.Username,
                Password = user.Password,
                Fullname = user.Fullname,
                Phone = user.Phone,
                Email = user.Email,
                IsSystemAdmin = user.IsSystemAdmin,
            };
            int idUser1 = await repository.Insert_(temp);
            UserViewModel user1 = new UserViewModel()
            {
                Id = idUser1,
                Username = user.Username,
                Password = user.Password,
                Fullname = user.Fullname,
                Phone = user.Phone,
                Email = user.Email,
                IsSystemAdmin = user.IsSystemAdmin,
                Status = true,
            };
            return user1;
        }

        public async Task<bool> Update(UserViewModel user)
        {
            User temp = await repository.Get_By_Id(user.Id);
            if (temp == null)
            {
                throw new UserIDNotFoundException("The user is not exist");
            }
            temp.Username = user.Username;
            temp.Password = user.Password;
            temp.Fullname = user.Fullname;
            temp.Phone = user.Phone;
            temp.Email = user.Email;
            temp.IsSystemAdmin = user.IsSystemAdmin;
            temp.Status = user.Status;
            return await repository.Update_(temp);
        }

        public async Task<bool> UpdateStatus(int id)
        {
            User temp = await repository.Get_By_Id(id);
            if (temp == null)
            {
                throw new UserIDNotFoundException("The user is not exist");
            }
            if (temp.Status == false)
            {
                throw new UserDeletedException("Deleted failed. The user was deleted");
            }
            temp.Username = temp.Username;
            temp.Password = temp.Password;
            temp.Fullname = temp.Fullname;
            temp.Phone = temp.Phone;
            temp.Email = temp.Email;
            temp.IsSystemAdmin = temp.IsSystemAdmin;
            temp.Status = false;
            return await repository.Update_(temp);
        }

        public async Task<bool> Delete(int id)
        {
            User temp = await repository.Get_By_Id(id);
            if (temp == null)
            {
                throw new ResidentIDNotFoundException("The user is not exist");
            }
            //repository.RemoveResidentRelationship(id);
            repository.RemoveResidentRelationship(id);
            return await repository.Delete_(temp);
        }

        public async Task<UserViewModel> GetUserByEmail(string email)
        {
            User user = null;
            bool checking = await repository.CheckExistedUser(email);
            //string newId = "";
            if (!checking)
            {
                return null;
            }
            user = await repository.GetUserByEmail(email);
            UserViewModel userView = new UserViewModel()
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Fullname = user.Fullname,
                Phone = user.Phone,
                Email = user.Email,
                IsSystemAdmin = user.IsSystemAdmin,
                Status = user.Status
            };
            return userView;
        }

        public bool CheckSystemAdmin(int id)
        {
            return repository.CheckSystemAdmin(id);
        }

        public bool CheckExistedUser(int id)
        {
            return repository.CheckExistedUser(id);
        }
    }
}
