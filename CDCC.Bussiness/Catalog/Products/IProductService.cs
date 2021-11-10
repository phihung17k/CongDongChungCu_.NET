using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Products;
using CDCC.Data.ViewModels.Products;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Products
{
    public interface IProductService
    {

        //trong đây thực hiện các tác vụ song song
        //get Product by id

        //Task<PagedResult<ViewProduct>> GetAllProduct();

        

        Task<ViewProducts> InsertProduct(InsertProductModel product);

        Task<bool> UpdateProduct(UpdateProductModel upProduct);

        Task<bool> DeleteProduct(int productID);

        //////////////////////////////////////
        // GET PRODUCT PAGING 
        //Task<PagedResult<ViewProduct>> GetProductPaging(PagingRequestBase condition);


        //Get Product By Store Id has filter product
        //ViewProducts là của data
        PagingResult<ViewProducts> Get_Product_By_StoreId(GetProductPagingRequest request);

        Task<ViewProducts> GetProductById(int id);

    }
}
