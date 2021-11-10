using CDCC.Bussiness.ViewModels.Building;
using CDCC.Data.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Buildings
{
    public interface IBuildingService
    {
        Task<List<BuildingViewModel>> GetAll();
        Task<BuildingViewModel> Get(int id);
        Task<BuildingViewModel> Insert(BuildingInsertModel building);
        Task<bool> Update(BuildingViewModel Building);
        Task<bool> UpdateStatus(int id);
        Task<bool> Delete(int id);
    }
}
