using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels.Poi;
using CDCC.Data.Common;

namespace CDCC.Bussiness.Catalog.PoiSvc
{
    public interface IPoiService
    {
        public PagingResult<PoiViewModel> GetByCondition(GetPoiPagingRequest request);
        public PoiViewModel GetById(int id);
        public int Insert(PoiInsertModel model);
        public bool Update(PoiUpdateModel model);
        public bool Delete(int id);
    }
}
