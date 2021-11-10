using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Stores;
using System.Threading.Tasks;


namespace CDCC.Bussiness.Catalog.Stores
{
    public interface IStoreService
    {
        //CREATE UPDATE DELETE STORE
        Task<ViewStores> InsertStore(InsertStoreModel store);

        Task<bool> UpdateStore(UpdateStoreModel store);

        Task<bool> DeleteStore(int storeId);


        //đang dùng view model tầng data
        public PagingResult<ViewStores> GetByCondition(GetStorePagingRequest request);
        //
        public Task<ViewStores> GetStoreById(int StoreId);

        public ViewStores GetStoreById_ResidentId(GetStore request);

    }
}
