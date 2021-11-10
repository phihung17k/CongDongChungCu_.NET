using CDCC.Data.Common;
using CDCC.Data.Models.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDCC.Data.Repository
{
    public class PoiRepository : Repository<Poi>, IPoiRepository
    {
        public PoiRepository(CongDongChungCuContext context)
            : base(context)
        {

        }

        public PagingResult<Poi> GetByCondition(GetPoiPagingRequest request)
        {
            try
            {
                //1. Select join
                var query = from p in context.Pois
                            join pt in context.Poitypes on p.PoitypeId equals pt.Id
                            join a in context.Apartments on p.ApartmentId equals a.Id
                            orderby p.Id descending
                            select new { p, pt, a };
                //2. Search
                if (request.Name != null && !request.Name.Equals(""))
                {
                    query = query.Where(x => x.p.Name.Contains(request.Name));
                }
                if (request.Status.HasValue)
                {
                    query = query.Where(x => x.p.Status.Equals(request.Status));
                }
                if (request.PoitypeId.HasValue && request.PoitypeId > 0)
                {
                    query = query.Where(x => x.pt.Id == request.PoitypeId);
                }
                if (request.ApartmentId.HasValue && request.ApartmentId > 0)
                {
                    query = query.Where(x => x.a.Id == request.ApartmentId);
                }
                //3. Paging
                int totalRow = query.Count();
                if (totalRow == 0) throw new NullReferenceException("Not found the given identity");
                var items = query.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize)
                    .Select(x => new Poi()
                    {
                        Id = x.p.Id,
                        Name = x.p.Name,
                        Address = x.p.Address,
                        Phone = x.p.Phone,
                        Status = x.p.Status,
                        PoitypeId = x.pt.Id,
                        ApartmentId = x.a.Id
                    }).ToList();
                return new PagingResult<Poi>(items, totalRow, request.currentPage, request.pageSize);
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
