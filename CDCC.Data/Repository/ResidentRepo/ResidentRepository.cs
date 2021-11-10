using CDCC.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Repository.ResidentRepo
{
    public class ResidentRepository : Repository<Resident>, IResidentRepository
    {
        public ResidentRepository(CongDongChungCuContext context) : base(context)
        {
        }

        public List<Resident> GetResidents(int? userId, bool? status)
        {
            //check status apartment, check status resident
            //default status = true
            List<Resident> list = new List<Resident>();
            var query = from r in context.Residents
                        select r;
            if (userId.HasValue)
            {
                query = query.Where(x => x.UserId == userId);
            }
            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status);
            }
            list = query.ToList();
            return list;
        }

        public List<int> GetAllResidentId(int userId)
        {
            List<int> list = new List<int>();
            var query = from r in context.Residents
                        where userId == r.UserId
                        select r.Id;
            if (query.Count() > 0)
            {
                list = query.ToList();
            }
            return list;
        }

        public List<Resident> GetResidents(int? userId)
        {
            List<Resident> list = new List<Resident>();
            var query = from r in context.Residents
                        select r;
            if (userId.HasValue)
            {
                query = query.Where(x => x.UserId == userId);
            }
            list = query.ToList();
            return list;
        }
    }
}
