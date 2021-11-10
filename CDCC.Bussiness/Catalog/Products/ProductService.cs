using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Enum;
using CDCC.Data.Common.Products;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository.ProductRepository;
using CDCC.Data.ViewModels.Products;
using System;
using System.Threading.Tasks;


namespace CDCC.Bussiness.Catalog.Products
{


    public class ProductService : IProductService
    {
        //goi IGenericRepository
        IProductRepository _genericRepositoryProduct;

        CongDongChungCuContext _context;
        public ProductService(IProductRepository genericRepositoryProduct, CongDongChungCuContext context)
        {
            _genericRepositoryProduct = genericRepositoryProduct;
            //
            _context = context;
        }


        //DELETE PRODUCT
        public async Task<bool> DeleteProduct(int productID)
        {
            try
            {
                Boolean check = false;
                //
                Product pro = await _genericRepositoryProduct.Get_By_Id(productID);
                //
                if (pro != null)
                {
                    //
                    check = await _genericRepositoryProduct.Delete_(pro);
                    //
                    return check;
                }
                //
                return check;
            }
            catch (Exception) { throw; }
        }


        //INSERT PRODUCT
        public async Task<ViewProducts> InsertProduct(InsertProductModel productModel)
        {
            try
            {

                ViewProducts vp = new ViewProducts();

                Product pro = new Product()
                {
                    //Id = productModel.Id , 
                    Price = productModel.Price,
                    Description = productModel.Description,
                    // mặc định insert thì sẽ là not approved
                    Status = Status.NotApproved,
                    //
                    CategoryId = productModel.CategoryId,
                    StoreId = productModel.StoreId,
                    Name = productModel.Name
                };

                int check = await _genericRepositoryProduct.Insert_(pro);

                //                                 RETURN DATA WHEN INSERT
                if (check > 0)
                {
                    Product product = await _genericRepositoryProduct.Get_By_Id(check);

                    // gọi load lazy
                    var test = _context.Entry(product);
                    //
                    test.Reference((Product p) => p.Category).Load();
                    //

                    if (product != null)
                    {
                        vp = new ViewProducts
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            Status = product.Status,
                            Description = product.Description,
                            StoreId = product.StoreId,
                            NameStore = _genericRepositoryProduct.loadLazing(product),
                            CategoryId = product.CategoryId,
                            CategoryName = product.Category.Name
                        };
                    }
                    
                }
                return vp;
            }
            catch (Exception) { throw; }
        }

        //UPDATE PRODUCT
        public async Task<bool> UpdateProduct(UpdateProductModel productModel)
        {
            try
            {
                //
                Boolean check = false;
                //get để cho ef set cho entity này trạng thái
                Product pro = await _genericRepositoryProduct.Get_By_Id(productModel.Id);

                //check xem có null không
                if (pro != null)
                {
                    //
                    pro.Price = (double)((productModel.Price.HasValue) ? productModel.Price : pro.Price);

                    pro.Description = (!productModel.Description.Equals("")) ? productModel.Description : pro.Description;

                    pro.Status = productModel.Status;

                    check = await _genericRepositoryProduct.Update_(pro);
                    return check;
                }
                return check;
            }
            catch (Exception) { throw; }

        }


        //GET PRODUCT BY ID
        public async Task<ViewProducts> GetProductById(int id)
        {
            try
            {
                //
                Product product = await _genericRepositoryProduct.Get_By_Id(id);
                //
                ViewProducts proView = null;
                //
                if (product != null)
                {
                    proView = new ViewProducts
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Status = product.Status,
                        Description = product.Description,
                        StoreId = product.StoreId,
                        NameStore = product.Store.Name,
                        CategoryId = product.CategoryId,
                        CategoryName = product.Category.Name
                    };
                }
                return proView;
            }
            catch (Exception) { throw; }
        }





        //GET PRODUCT BY STORE ID
        public PagingResult<ViewProducts> Get_Product_By_StoreId(GetProductPagingRequest request)
        {
            try
            {
                //
                PagingResult<ViewProducts> result = _genericRepositoryProduct.Get_List_Product_By_StoreID(request);
                //
                return result;
            }
            catch (Exception) { throw; }
        }




        //GET PRODUCT PAGING
        //public async Task<PagedResult<ViewProduct>> GetProductPaging(PagingRequestBase condition)
        //{
        //    //Take indexPage and sizePage in PagingRequestBase
        //    int indexPage = condition.PageIndex;
        //    int sizePage = condition.PageSize;

        //    //
        //    List<Product> listProduct = await _genericRepositoryProduct.Get_By_Paging(indexPage, sizePage);

        //    //filter follow by price
        //    var listProductFilter = from p in listProduct
        //                            orderby p.Price ascending
        //                            select p;


        //    //add in ViewProduct
        //    List<ViewProduct> listViewProduct = new List<ViewProduct>();

        //    listProductFilter.ToList().ForEach(product => {
        //        ViewProduct vp = new ViewProduct();
        //        //set
        //        vp.Id = product.Id;
        //        vp.Name = product.Name;
        //        vp.Price = product.Price;
        //        vp.Status = product.Status;
        //        vp.Description = product.Description;
        //        vp.CategoryName = product.Category.Name;
        //        //
        //        listViewProduct.Add(vp);
        //    });

        //    //
        //    PagedResult<ViewProduct> result = new PagedResult<ViewProduct>(listViewProduct,listViewProduct.Count);
        //    //
        //    return result;
        //}


        // GET ALL PRODUCT
        //public async Task<PagedResult<ViewProduct>> GetAllProduct()
        //{
        //    List<ViewProduct> list = new List<ViewProduct>();

        //    //trả ra tất cả record
        //    List<Product> listProduct = await _genericRepositoryProduct.Get_All();



        //    //list product có status true lọc theo Price
        //    var listProduct_true = from Product p in listProduct
        //                           where p.Status == true
        //                           orderby p.Price ascending
        //                           select p;


        //    ////list product PriceFrom 2 - PriceTo 5 và có status true
        //    //var listProduct_true = from Product p in listProduct
        //    //                       where p.Status == true && p.Price > 2 && p.Price < 5
        //    //                       orderby p.Price ascending
        //    //                       select p;

        //    // add những thứ cần thiết vào ViewProduct
        //    listProduct_true.ToList().ForEach(product => {
        //        ViewProduct vp = new ViewProduct();
        //        //set
        //        vp.Id = product.Id;
        //        vp.Name = product.Name;
        //        vp.Price = product.Price;
        //        vp.Status = product.Status;
        //        vp.Description = product.Description;
        //        //
        //        list.Add(vp);
        //    });

        //    //
        //    return new PagedResult<ViewProduct>()
        //    {
        //        Items = list,
        //        TotalRecord = list.Count
        //    };
        //}


    }
}
