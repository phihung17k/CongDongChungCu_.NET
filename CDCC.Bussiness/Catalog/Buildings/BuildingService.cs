using CDCC.Bussiness.Catalog.Buildings;
using CDCC.Bussiness.ViewModels.Building;
using CDCC.Data.CustomException;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Buildings
{
    public class BuildingService : IBuildingService 
    {
        private IRepository<Building> repository;

        public BuildingService(IRepository<Building> repository)
        {
            this.repository = repository;
        }

        public async Task<List<BuildingViewModel>> GetAll()
        {
            List<BuildingViewModel> buildingViews = new List<BuildingViewModel>();
            List<Building> buildings = await repository.Get_All();
            foreach (Building temp in buildings)
            {
                buildingViews.Add(new BuildingViewModel()
                {
                    Id = temp.Id,
                    Name = temp.Name,
                    ApartmentId = temp.ApartmentId,
                    Status = temp.Status
                });
            }
            return buildingViews;
        }
        public async Task<BuildingViewModel> Get(int id)
        {
            Building building = await repository.Get_By_Id(id);
            if (building == null)
            {
                throw new BuildingIDNotFoundException("Not found building by this id");
            }
            return new BuildingViewModel()
            {
                Id = building.Id,
                Name = building.Name,
                ApartmentId = building.ApartmentId,
                Status = building.Status
            };
        }

        public async Task<BuildingViewModel> Insert(BuildingInsertModel building)
        {
            Building temp = new Building()
            {
                Name = building.Name,
                ApartmentId = building.ApartmentId
            };
            int idBuilding1 = await repository.Insert_(temp);
            BuildingViewModel Building1 = new BuildingViewModel()
            {
                Id = idBuilding1,
                Name = building.Name,
                ApartmentId = building.ApartmentId,
                Status = true
            };
            return Building1;
        }

        public async Task<bool> Update(BuildingViewModel building)
        {
            Building temp = await repository.Get_By_Id(building.Id);
            if (temp == null)
            {
                throw new BuildingIDNotFoundException("The Building is not exist");
            }
            temp.Name = building.Name;
            temp.ApartmentId = building.ApartmentId;
            temp.Status = building.Status;
            return await repository.Update_(temp);
        }

        public async Task<bool> UpdateStatus(int id)
        {
            Building temp = await repository.Get_By_Id(id);
            if (temp == null)
            {
                throw new BuildingIDNotFoundException("The Building is not exist");
            }
            if (temp.Status == false)
            {
                throw new BuildingDeletedException("Deleted failed. The Building was deleted");
            }
            temp.Name = temp.Name;
            temp.ApartmentId = temp.ApartmentId;
            temp.Status = false;
            return await repository.Update_(temp);
        }

        public async Task<bool> Delete(int id)
        {
            Building temp = await repository.Get_By_Id(id);
            if (temp == null)
            {
                throw new BuildingIDNotFoundException("The buidling is not exist");
            }
            return await repository.Delete_(temp);
        }
    }
}
