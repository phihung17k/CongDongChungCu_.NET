using CDCC.Data.Common;
using CDCC.Data.Common.Enum;
using CDCC.Data.Common.Products;
using CDCC.Data.Models.DB;
using CDCC.Data.ViewModels.Products;
using System;
using System.Collections.Generic;

namespace CDCC.Data.Repository.ProductRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        //Lọc sản phẩm
        //chỗ này 
        //trả ra dữ liệu entity với nên lên service mới chuyển sang model
        List<Product> GetCondition_Name_Price_Category(Status? status,
                                                        String? nameOfProduct,
                                                        double? MoneyFrom, double? MoneyTo,
                                                        String? CategoryName,
                                                        int? CategoryId,
                                                        List<Product> Products_To_Filter);


        //lấy sản phẩm theo storeID có filter
        PagingResult<ViewProducts> Get_List_Product_By_StoreID(GetProductPagingRequest request);


        public String loadLazing(Product product);
    }
}
