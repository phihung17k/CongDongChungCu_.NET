using CDCC.Data.Common;
using CDCC.Data.Common.Stores;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository.ProductRepository;
using CDCC.Data.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Repository.StoreRepository
{
    public class StoreRepository : Repository<Store>, IStoreRepository 
    {
        //muốn lấy phương thức lọc dữ liệu PRODUCT từ bên ProductRepo
        IProductRepository _productRepository;

        public StoreRepository(CongDongChungCuContext context, IProductRepository productRepository)
            : base(context)
        {
            _productRepository = productRepository;
        }

        //Hàm LazyLoad
        public string loadLazing (Store store)
        {
            // gọi load lazy
            var test = context.Entry(store);
            //
            test.Reference((Store st) => st.Resident).Load();
            //
            var userId = store.Resident.UserId;
            //
            var FullName = from User us in context.Users
                           where us.Id == userId
                           select us.Fullname;

            string OwnerName = FullName.ToList()[0];

            return OwnerName;
        }



        //GET BY CONDITION (tùy biến)
        public PagingResult<ViewStores> GetByCondition(GetStorePagingRequest request)
        {
                                                         // SET VIEW MODEL
            //ViewStore
            List<ViewStores> listView_Store = new List<ViewStores>();
            //total count store
            int StoreCount = 0;

            

            //bắt đầu truy xuất
            //1. lấy tất cả list Store của 1 Apartment
            var Store_Apartment = from store in context.Stores
                                       where store.ApartmentId == request.ApartmentId
                                       select store;

            //                                           //STORE Belong to specific Apartment ID
            //lọc theo request filter

            //lọc theo NAME OF STORE
            if (request.NameOfStore != null)
            {
                //return
                var List_Store_Apartment_NameOfStore = Store_Apartment.Where((Store st) => st.Name.Contains(request.NameOfStore));
                //
                StoreCount = List_Store_Apartment_NameOfStore.ToList().Count;
                // paging 
                Store_Apartment = List_Store_Apartment_NameOfStore.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize);
                //
            }
            else
            {
                //
                StoreCount = Store_Apartment.ToList().Count;
                //paging
                Store_Apartment = Store_Apartment.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize);
            }

            ////STORE ID
            ////nếu có store củ thể thì search store cụ thể
            //if(request.StoreId.HasValue)
            //{
            //    //này đang query trên một bảng kh hổ trợ find chỉ có những câu sql
            //   var Store_ID = Store_Apartment.Where((Store st) => st.Id == request.StoreId);
            //    //
            //    Store_Apartment = Store_ID;
            //    //
            //    StoreCount = 1;
            //}

            //Store Paging
            //bao nhiều store được hiện lên trên màn hình
            //if (!request.StoreId.HasValue)
            //{
                
                
            //}


                                                      //List PRODUCT has filter and Belong to Store

            //ta lấy giá trị Store_Apartment

            //mỗi store sẽ có 1 list product
            Store_Apartment.ToList().ForEach((Store store) => {

                
                //                                    Product filter
                //lấy tất cả sản phẩm trong 1 store
               // List<Product> List_Product_In_Store = store.Products.ToList();

              //  List_Product_In_Store = _productRepository.GetCondition_Name_Price_Category(request.NameOfProduct,
                                                                                            //request.PriceFrom,
                                                                                            //request.PriceTo,
                                                                                            //request.CategoryName,
                                                                                            //List_Product_In_Store);
                // sau khi có được listProduct filter - Store
                //chuẩn bị trả ra View

                //Set View Store
                ViewStores vs = new ViewStores();
                vs.StoreId = store.Id;
                vs.Name = store.Name;
                vs.Address = store.Address;
                vs.OpeningTime = store.OpeningTime;
                vs.ClosingTime = store.ClosingTime;
                vs.Phone = store.Phone;
                vs.Status = (bool)store.Status;
                vs.OwnerStore = store.Resident.User.Fullname;
                listView_Store.Add(vs);
                //if (returnDataWhenInsert)
                //{
                //    // gọi load lazy
                //    var test = context.Entry(store);
                //    //
                //    test.Reference((Store st) => st.Resident).Load();
                //    //
                //    var userId = store.Resident.UserId;
                //    //
                //    var FullName = from User us in context.Users
                //                   where us.Id == userId
                //                   select us.Fullname;

                //    vs.OwnerStore = FullName.ToList()[0];

                //}
                //else
                //{

                //}

                //set cho listView_Store List<ViewStore>


                //thêm list product của store qua ViewModel
                //duyệt qua từng product trả ra ViewProduct
                //List<ViewProducts> listView_Product = new List<ViewProducts>();

                //List_Product_In_Store.ForEach((Product p) => {
                //    //ViewProduct
                //    ViewProducts vp = new ViewProducts();
                //    vp.Id = p.Id;
                //    vp.Name = p.Name;
                //    vp.Price = p.Price;
                //    vp.Description = p.Description;
                //    //
                //    vp.CategoryName = p.Category.Name;
                //    vp.Status = (bool)p.Status;

                //    //add to list ViewProduct
                //    listView_Product.Add(vp);

                //    //add countProduct


                //});//end foreach List_Product_In_Store



                //vs.listProduct = listView_Product;    //end set view cho ViewStore
                //vs.ProductCount = vs.listProduct.Count;//add countProduct in store

                //lấy mặc định 5 product
                //vs.TotalPageOfProduct = (int)Math.Ceiling(vs.ProductCount / (double)vs.PageSizeProdcuts);
                // phân trang cho product luôn
                ///vs.listProduct = vs.listProduct.Skip((vs.IndexPageProduct - 1) * vs.PageSizeProdcuts).Take(vs.PageSizeProdcuts).ToList();



            });// end Store_Apartment


            return new PagingResult<ViewStores>(listView_Store, StoreCount, request.currentPage, request.pageSize);

        }

        public ViewStores getStoreByResidentId(int residentId)
        {
            ViewStores vs = new ViewStores();

            Store store = (from st in context.Stores
                                  where st.ResidentId == residentId
                                  select st).FirstOrDefault();

            if(store != null)
            {
                vs.StoreId = store.Id;
                vs.Name = store.Name;
                vs.Address = store.Address;
                vs.OpeningTime = store.OpeningTime;
                vs.ClosingTime = store.ClosingTime;
                vs.Phone = store.Phone;
                vs.Status = (bool)store.Status;
                vs.OwnerStore = store.Resident.User.Fullname;

                return vs;
            }
            else
            {
                return null;
            }

        }
    }
}
