using CDCC.Data.Common;
using CDCC.Data.Models.DB;

namespace CDCC.Data.Repository
{
    public interface INewsRepository : IRepository<News>
    {
        public PagingResult<News> GetByCondition(GetNewsPagingRequest request);
    }
}
