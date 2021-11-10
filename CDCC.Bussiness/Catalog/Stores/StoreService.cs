using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Stores;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository.StoreRepository;
using System;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Stores
{
    public class StoreService : IStoreService
    {


        //goi IGenericRepository
        IStoreRepository _genericRepositoryStore;

        public StoreService(IStoreRepository genericRepositoryStore)
        {
            _genericRepositoryStore = genericRepositoryStore;
        }

       
        //INSERT
        public async Task<ViewStores> InsertStore(InsertStoreModel store)
        {
            try {

                
            //new store
            Store sto = new Store()
            {

                //Name
                Name = store.Name,
                //Opening time
                OpeningTime = store.OpeningTime,
                //Closing time
                ClosingTime = store.ClosingTime,
                //Address
                Address = store.Address,
                //Phone
                Phone = store.Phone,
                //Status
                Status = true,
                //Resident ID
                ResidentId = store.ResidentId,
                //Apartment ID
                ApartmentId = store.ApartmentId
                //
                
            };
            int result = await _genericRepositoryStore.Insert_(sto);

            //                                      RETURN DATA WHEN INSERT
            if(result > 0)
            {
                    Store st = await _genericRepositoryStore.Get_By_Id(result);
                    //Set ViewModels
                    //Set model View
                    ViewStores vt = new ViewStores();
                    //
                    vt.StoreId = st.Id;
                    //
                    vt.Name = st.Name;
                    //
                    vt.Address = st.Address;
                    //
                    vt.OpeningTime = st.OpeningTime;
                    //
                    vt.ClosingTime = st.ClosingTime;
                    //
                    vt.Phone = st.Phone;
                    //
                    vt.OwnerStore = _genericRepositoryStore.loadLazing(st);
                    //
                    vt.Status = (bool)store.Status;
                    //
                    return vt;
                }

            return null;
            }
            catch (Exception) { throw; }

        }
        //update store
        public async Task<bool> UpdateStore(UpdateStoreModel store)
        {
            try {
            //get store
            Store kq = await _genericRepositoryStore.Get_By_Id(store.StoreId);
            //
            Boolean check = false;
                //update
                //
            if (kq != null)
            {
             //name
             kq.Name = (!store.Name.Equals(""))? store.Name : kq.Name ;

            kq.OpeningTime = (DateTime)((store.OpeningTime.HasValue) ? store.OpeningTime : kq.OpeningTime);

            kq.ClosingTime = (DateTime)((store.ClosingTime.HasValue) ? store.ClosingTime : kq.ClosingTime);

            kq.Address = (!store.Address.Equals("")) ? store.Address : kq.Address;

            kq.Phone = (!store.Phone.Equals("")) ? store.Phone : kq.Phone;

            kq.Status = store.Status;

            
                check = await _genericRepositoryStore.Update_(kq);
            }
            return check;
            }
            catch (Exception) { throw; }
        }

        //GET List Store - 1 Store belong to 1 apartment id 
        public PagingResult<ViewStores> GetByCondition(GetStorePagingRequest request)
        {
            try { 
            // RETURN DATA WHEN INSERT false
            return _genericRepositoryStore.GetByCondition(request);
            }
            catch (Exception) { throw; }
        }


        //DELETE STORE
        public async Task<bool> DeleteStore(int StoreId)
        {
            try { 
            //get 
            Store store = await _genericRepositoryStore.Get_By_Id(StoreId);
            //
            Boolean check = false;
                //
                if (store != null)
                {
                    check = await _genericRepositoryStore.Delete_(store);
                    //
                }
            return check;
            }
            catch (Exception) { throw; }
        }

        //GET STORE BY ID
        public async Task<ViewStores> GetStoreById(int StoreId)
        {
            //
            try
            {
                //get 
                Store store = await _genericRepositoryStore.Get_By_Id(StoreId);
                //Set model View
                if (store != null)
                {
                    ViewStores vt = new ViewStores();
                    //
                    vt.StoreId = store.Id;
                    //
                    vt.Name = store.Name;
                    //
                    vt.Address = store.Address;
                    //
                    vt.OpeningTime = store.OpeningTime;
                    //
                    vt.ClosingTime = store.ClosingTime;
                    //
                    vt.Phone = store.Phone;
                    //
                    vt.OwnerStore = store.Resident.User.Fullname;
                    //
                    vt.Status = (bool)store.Status;
                    //
                    return vt;
                }
                return null;
            }
            catch (Exception) { throw; }
        }

        public ViewStores GetStoreById_ResidentId(GetStore request)
        {


            if(request.StoreId != 0)
            {
                Console.WriteLine("có store id");
                return GetStoreById(request.StoreId).Result;
            }

            if (request.ResidentId != 0) {

                Console.WriteLine("có resident id");
                return _genericRepositoryStore.getStoreByResidentId(request.ResidentId);
            
            }

            return null;
        }
    }
    
}
