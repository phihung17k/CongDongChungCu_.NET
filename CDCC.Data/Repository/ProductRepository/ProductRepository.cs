using CDCC.Data.Common;
using CDCC.Data.Common.Enum;
using CDCC.Data.Common.Products;
using CDCC.Data.Models.DB;
using CDCC.Data.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDCC.Data.Repository.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(CongDongChungCuContext context)
           : base(context)
        {

        }

        //Phục vụ cho 1 STORE
        public List<Product> GetCondition_Name_Price_Category(Status? status,
                                                               string? nameOfProduct,
                                                               double? MoneyFrom,
                                                               double? MoneyTo,
                                                               string? CategoryName,
                                                               int? CategoryId,
                                                               List<Product> Products_To_Filter)
        {
            //Status
            //Trong list Product sẽ có những bài post đc Rejected, NotApproved, Approved, InActive
            //Rejected
            if (status == Status.Rejected)
            {
                Products_To_Filter = Products_To_Filter.Where((Product p) => p.Status == Status.Rejected).ToList();
            }

            //NotApproved
            if (status == Status.NotApproved)
            {
                Products_To_Filter = Products_To_Filter.Where((Product p) => p.Status == Status.NotApproved).ToList();
            }

            //Approved
            if (status == Status.Approved)
            {
                Products_To_Filter = Products_To_Filter.Where((Product p) => p.Status == Status.Approved).ToList();
            }

            //Inactive
            if (status == Status.InActive)
            {
                Products_To_Filter = Products_To_Filter.Where((Product p) => p.Status == Status.InActive).ToList();
            }


            //if Name
            if (nameOfProduct != null)
            {
                //này là tương tác với list r kh phải Iqueryable
                var query = from Product p in Products_To_Filter
                            where p.Name.Contains(nameOfProduct) //p.Name == nameOfProduct
                            select p;
                //update list filter
                Products_To_Filter = query.ToList();

            }

            //if MoneyTo - MoneyFrom  from 3000 to 5000
            if (MoneyFrom.HasValue && MoneyTo.HasValue)
            {
                var query = from Product p in Products_To_Filter
                            where p.Price > MoneyFrom && p.Price < MoneyTo
                            select p;
                //update list filter
                Products_To_Filter = query.ToList();
            }

            //if has Category
            if (CategoryName != null)
            {
                var query = from Product p in Products_To_Filter
                            where p.Category.Name.Contains(CategoryName)
                            select p;
                //update list filter
                Products_To_Filter = query.ToList();
            }


            if (CategoryId != null)
            {
                var query = from Product p in Products_To_Filter
                            where p.CategoryId == CategoryId
                            select p;
                //update list filter
                Products_To_Filter = query.ToList();
            }



            return Products_To_Filter;
        }


        // Phục vụ cho request
        public PagingResult<ViewProducts> Get_List_Product_By_StoreID(GetProductPagingRequest request)
        {
            //List product view
            List<ViewProducts> listView_Product = new List<ViewProducts>();
            //set product count
            int productCount = 0;
            //model trả về 
            PagingResult<ViewProducts> result;

            //check store id hasn't value
            //
            if (!request.StoreId.HasValue)
            {

                //load theo page size sản phẩm trong db
                List<Product> list_product = new List<Product>();

                //Status
                //Trong list Product sẽ có những bài post đc Rejected, NotApproved, Approved, InActive
                if (request.Status == Status.Rejected)
                {
                    //theo status trước
                    list_product = context.Products.Where((Product p) => p.Status == Status.Rejected).ToList();
                    //lọc theo điều kiện
                    list_product = GetCondition_Name_Price_Category(request.Status, request.NameOfProduct, request.PriceFrom, request.PriceTo, request.CategoryName, request.CategoryId, list_product).ToList();
                    //sau đó list product count mới chính xác 
                    productCount = list_product.Count;

                    list_product = list_product.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();
                }

                //
                if (request.Status == Status.NotApproved)
                {
                    list_product = context.Products.Where((Product p) => p.Status == Status.NotApproved).ToList();
                    //
                    list_product = GetCondition_Name_Price_Category(request.Status, request.NameOfProduct, request.PriceFrom, request.PriceTo, request.CategoryName, request.CategoryId, list_product).ToList();

                    productCount = list_product.Count;

                    list_product = list_product.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();
                }

                //Approved
                if (request.Status == Status.Approved)
                {
                    list_product = context.Products.Where((Product p) => p.Status == Status.Approved).ToList();
                    //
                    list_product = GetCondition_Name_Price_Category(request.Status, request.NameOfProduct, request.PriceFrom, request.PriceTo, request.CategoryName, request.CategoryId, list_product).ToList();

                    productCount = list_product.Count;

                    list_product = list_product.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();
                }

                //
                if (request.Status == Status.InActive)
                {
                    list_product = context.Products.Where((Product p) => p.Status == Status.InActive).ToList();
                    
                    //Filter list
                    list_product = GetCondition_Name_Price_Category(request.Status, request.NameOfProduct, request.PriceFrom, request.PriceTo, request.CategoryName, request.CategoryId, list_product).ToList();

                    productCount = list_product.Count;

                    list_product = list_product.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();
                    

                }
                //
                if (!request.Status.HasValue)
                {
                    list_product = context.Products.ToList();

                    //
                    list_product = GetCondition_Name_Price_Category(request.Status, request.NameOfProduct, request.PriceFrom, request.PriceTo, request.CategoryName, request.CategoryId, list_product).ToList();
                                  
                    productCount = list_product.Count;


                    list_product = list_product.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();

                }

                //model
                //set product count
                //productCount = list_product.Count;

                //chuẩn bị trả ra listproduct view

                list_product.ForEach(pro =>
                {

                    //ViewProduct
                    ViewProducts vp = new ViewProducts();
                    vp.Id = pro.Id;
                    vp.Name = pro.Name;
                    vp.Price = pro.Price;
                    vp.Description = pro.Description;
                    vp.StoreId = pro.StoreId;
                    vp.CategoryId = pro.CategoryId;
                    vp.CategoryName = pro.Category.Name;
                    vp.NameStore = pro.Store.Name;
                    vp.Status = pro.Status;
                    //
                    listView_Product.Add(vp);
                });

                result = new PagingResult<ViewProducts>(listView_Product, productCount, request.currentPage, request.pageSize);

            }
            //has store id value
            else
            {
                //get StoreID 
                var query = from Store st in context.Stores
                            where st.Id == request.StoreId
                            select st;


                //trong list lúc này chỉ có 1 store
                query.ToList().ForEach((Store st) =>
                {

                    //Lazy Loading navigation collection
                    List<Product> list_product = st.Products.ToList();

                    //filter Product 
                    list_product = GetCondition_Name_Price_Category(request.Status, request.NameOfProduct, request.PriceFrom, request.PriceTo, request.CategoryName, request.CategoryId, list_product);

                    //set product count
                    productCount = list_product.Count;

                    //Paging product
                    //Skip cơ chế bỏ qua trang trước đó 
                    //Take là lấy số sp hiện lên trên 1 trang
                    list_product = list_product.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();

                    //chuẩn bị trả ra listproduct view

                    list_product.ForEach(pro =>
                    {

                        //ViewProduct
                        ViewProducts vp = new ViewProducts();
                        vp.Id = pro.Id;
                        vp.Name = pro.Name;
                        vp.Price = pro.Price;
                        vp.Description = pro.Description;
                        vp.StoreId = pro.StoreId;
                        vp.CategoryId = pro.CategoryId;
                        vp.CategoryName = pro.Category.Name;
                        vp.NameStore = pro.Store.Name;
                        vp.Status = pro.Status;
                        //
                        listView_Product.Add(vp);
                    });

                });

                //trả ra PagingResult
                //chỗ này kh thể dùng listView_Product.Count làm tham số thứ 2 
                //vì có paging nên listView_Product.Count sẽ trả ra số sp ở 1 trang chứ kh phải tổng sản phẩm
                result = new PagingResult<ViewProducts>(listView_Product, productCount, request.currentPage, request.pageSize);
            }

            return result;
        }

        public string loadLazing(Product product)
        {
            // gọi load lazy
            var test = context.Entry(product);
            //
            test.Reference((Product p) => p.Store).Load();
            //
            String nameStore = product.Store.Name;
            //
            //var FullName = from User us in context.Users
            // where us.Id == userId
            //select us.Fullname;
            //string OwnerPost = FullName.ToList()[0];

            return nameStore;
        }
    }
}
