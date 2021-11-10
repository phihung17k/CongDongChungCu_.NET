using CDCC.Data.Common;
using CDCC.Data.Models.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDCC.Data.Repository
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(CongDongChungCuContext context)
            : base(context)
        {

        }
        public PagingResult<News> GetByCondition(GetNewsPagingRequest request)
        {
            try
            {
                //1. Select join
                var query = from n in context.News
                            join a in context.Apartments on n.ApartmentId equals a.Id
                            orderby n.CreatedDate descending
                            select new { n, a };
                //2. Search
                if (request.Keyword != null && !request.Keyword.Equals(""))
                {
                    query = query.Where(x => x.n.Content.Contains(request.Keyword) || x.n.Title.Contains(request.Keyword));
                }
                if (request.FromDate.HasValue && request.ToDate.HasValue)
                {
                    query = query.Where(x => x.n.CreatedDate >= request.FromDate && x.n.CreatedDate <= request.ToDate);
                }
                if (request.Status.HasValue)
                {
                    query = query.Where(x => x.n.Status.Equals(request.Status));
                }
                if (request.ApartmentId.HasValue && request.ApartmentId > 0)
                {
                    query = query.Where(x => x.a.Id == request.ApartmentId);
                }
                //3. Paging
                int totalRow = query.Count();
                if (totalRow == 0) throw new NullReferenceException("Not found the given identity");
                var items = query.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize)
                    .Select(x => new News()
                    {
                        Id = x.n.Id,
                        Title = x.n.Title,
                        Content = x.n.Content,
                        CreatedDate = x.n.CreatedDate,
                        Status = x.n.Status,
                        ApartmentId = x.n.ApartmentId
                    }).ToList();
                return new PagingResult<News>(items, totalRow, request.currentPage, request.pageSize);
            } catch (SqlException)
            {
                throw;
            }
        }
    }
}
