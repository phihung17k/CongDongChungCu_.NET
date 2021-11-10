using CDCC.Data.Common;
using CDCC.Data.Models.DB;

namespace CDCC.Data.Repository
{
    public interface IPoiRepository : IRepository<Poi>
    {
        public PagingResult<Poi> GetByCondition(GetPoiPagingRequest request);
    }
}
