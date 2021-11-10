using CDCC.Bussiness.ViewModels.Resident;
using CDCC.Data.CustomException;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using CDCC.Data.Repository.ResidentRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Residents
{
    public class ResidentService : IResidentService
    {
        private IResidentRepository repository;

        public ResidentService(IResidentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<ResidentViewModel>> GetAll()
        {
            List<ResidentViewModel> residentViews = new List<ResidentViewModel>();
            List<Resident> residents = await repository.Get_All();
            foreach (Resident temp in residents)
            {
                residentViews.Add(new ResidentViewModel() { 
                    Id = temp.Id,
                    IsAdmin = temp.IsAdmin,
                    Status = temp.Status,
                    UserId = temp.UserId,
                    BuildingId = temp.BuildingId,
                    ApartmentId = temp.ApartmentId
                });
            }
            return residentViews;
        }

        public List<ResidentTokenModel> GetAll(int? userId)
        {
            List<ResidentTokenModel> residentTokens = new List<ResidentTokenModel>();
            List<Resident> residents = repository.GetResidents(userId);
            foreach (Resident temp in residents)
            {
                residentTokens.Add(new ResidentTokenModel()
                {
                    Id = temp.Id,
                    IsAdmin = temp.IsAdmin,
                    Status = temp.Status,
                    ApartmentId = temp.ApartmentId,
                    BuildingId = temp.BuildingId
                });
            }
            return residentTokens;
        }

        public List<ResidentTokenModel> GetAll(int? userId, bool? status)
        {
            List<ResidentTokenModel> residentTokens = new List<ResidentTokenModel>();
            List<Resident> residents = repository.GetResidents(userId, status);
            foreach (Resident temp in residents)
            {
                //system admin pass status = true / false
                if (status.HasValue)
                {
                    residentTokens.Add(new ResidentTokenModel()
                    {
                        Id = temp.Id,
                        IsAdmin = temp.IsAdmin,
                        Status = temp.Status,
                        ApartmentId = temp.ApartmentId,
                        BuildingId = temp.BuildingId
                    });
                } 
                else
                {
                    //mobile don't need pass status = true in query
                    residentTokens.Add(new ResidentTokenModel()
                    {
                        Id = temp.Id,
                        IsAdmin = temp.IsAdmin,
                        ApartmentId = temp.ApartmentId,
                        BuildingId = temp.BuildingId
                    });
                }
            }
            return residentTokens;
        }

        public async Task<ResidentViewModel> Get(int id)
        {
            Resident resident = await repository.Get_By_Id(id);
            if(resident == null)
            {
                throw new ResidentIDNotFoundException("Not found resident by this id");
            }
            return new ResidentViewModel() {
                Id = resident.Id,
                IsAdmin = resident.IsAdmin,
                Status = resident.Status,
                UserId = resident.UserId,
                BuildingId = resident.BuildingId,
                ApartmentId = resident.ApartmentId,
            };
        }

        public async Task<ResidentViewModel> Insert(ResidentInsertModel resident)
        {
            Resident temp = new Resident()
            {
                IsAdmin = resident.IsAdmin,
                UserId = resident.UserId,
                BuildingId = resident.BuildingId,
                ApartmentId = resident.ApartmentId
            };
            int idResident1 = await repository.Insert_(temp);
            ResidentViewModel resident1 = new ResidentViewModel()
            {
                Id = idResident1,
                IsAdmin = resident.IsAdmin,
                Status = true,
                UserId = resident.UserId,
                BuildingId = resident.BuildingId,
                ApartmentId = resident.ApartmentId
            };
            return resident1;
        }

        public async Task<bool> Update(ResidentViewModel resident)
        {
            Resident temp = await repository.Get_By_Id(resident.Id);
            if (temp == null)
            {
                throw new ResidentIDNotFoundException("The resident is not exist");
            }
            temp.IsAdmin = resident.IsAdmin;
            temp.Status = resident.Status;
            temp.UserId = resident.UserId;
            temp.BuildingId = resident.BuildingId;
            temp.ApartmentId = resident.ApartmentId;
            return await repository.Update_(temp);
        }

        //user click delete, set status = false
        public async Task<bool> UpdateStatus(int id)
        {
            Resident temp = await repository.Get_By_Id(id);
            if (temp == null)
            {
                throw new ResidentIDNotFoundException("The resident is not exist");
            }
            if(temp.Status == false)
            {
                throw new ResidentDeletedException("Deleted failed. The resident was deleted");
            }
            temp.IsAdmin = temp.IsAdmin;
            temp.Status = false;
            temp.UserId = temp.UserId;
            temp.BuildingId = temp.BuildingId;
            temp.ApartmentId = temp.ApartmentId;
            return await repository.Update_(temp);
        }

        public async Task<bool> Delete(int id)
        {
            Resident temp = await repository.Get_By_Id(id);
            if(temp == null)
            {
                throw new ResidentIDNotFoundException("The resident is not exist");
            }
            return await repository.Delete_(temp);
        }

        public List<int> GetAllResidentId(int userId)
        {
            List<int> residentIdList = repository.GetAllResidentId(userId);
            return residentIdList;
        }
    }
}
