using CDCC.Bussiness.ViewModels.Resident;
using CDCC.Data.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Residents
{
    public interface IResidentService
    {
        Task<List<ResidentViewModel>> GetAll();
        List<ResidentTokenModel> GetAll(int? userId, bool? status);
        List<ResidentTokenModel> GetAll(int? userId);
        Task<ResidentViewModel> Get(int id);
        Task<ResidentViewModel> Insert(ResidentInsertModel resident);
        Task<bool> Update(ResidentViewModel resident);
        Task<bool> UpdateStatus(int id);
        Task<bool> Delete(int id);
        List<int> GetAllResidentId(int userId);
    }
}
