using CDCC.Data.Common;
using CDCC.Data.Common.Stores;
using CDCC.Data.Models.DB;

namespace CDCC.Data.Repository.StoreRepository
{
    public interface IStoreRepository : IRepository<Store>
    {
        //
        public PagingResult<ViewStores> GetByCondition(GetStorePagingRequest request);

        public string loadLazing(Store store);

        public ViewStores getStoreByResidentId(int residentId);
        

    }
   
   
}
