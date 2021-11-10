using CDCC.Bussiness.ViewModels.Apartment;
using CDCC.Data.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Apartments
{
    public interface IApartmentService
    {
        Task<List<ApartmentViewModel>> GetAll();
        Task<ApartmentViewModel> Get(int id);
        Task<ApartmentViewModel> Insert(ApartmentInsertModel apartment);
        Task<bool> Update(ApartmentViewModel apartment);
        Task<bool> UpdateStatus(int id);
        Task<bool> Delete(int id);
    }
}
