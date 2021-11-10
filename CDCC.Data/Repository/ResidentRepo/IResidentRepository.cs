using CDCC.Data.Models.DB;
using System.Collections.Generic;

namespace CDCC.Data.Repository.ResidentRepo
{
    public interface IResidentRepository : IRepository<Resident>
    {
        List<Resident> GetResidents(int? userId, bool? status);

        List<Resident> GetResidents(int? userId);

        List<int> GetAllResidentId(int userId);
    }
}
