using CDCC.Data.Common;
using CDCC.Data.Common.Categories;
using CDCC.Data.Models.DB;
using CDCC.Data.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCC.Data.Repository.CategoryRepository
{
    public class CategoryRepository : Repository<Category>, ICateRepository
    {
        public CategoryRepository(CongDongChungCuContext context) : base(context)
        {
        }

        public async Task<CategoryResult> GetCategoryOfStore(GetCategoryRequest request)
        {
            List<ViewCategories> listCate = new List<ViewCategories>();
            CategoryResult result;

            if (request.StoreId.HasValue)
            {
                //get store id
                var query1 = context.Stores.Where((Store st) => st.Id == request.StoreId).ToList();
                Store store = query1[0];

                //lấy list product theo store
                List<Product> listPro = store.Products.ToList();

                //lấy ra category id
                var query2 = (from Product p in listPro
                              select p.CategoryId).Distinct();

                List<int> cateId = query2.ToList();

                //set model trả ra
                foreach (int id in cateId)
                {
                    //có được category theo id
                    Category cate = await TakeCategory(id);
                    //
                    ViewCategories vc = new ViewCategories();
                    vc.Id = cate.Id;
                    vc.Name = cate.Name;
                    //
                    listCate.Add(vc);
                }
                 result = new CategoryResult(listCate);
            }
            else
            {
                List<Category> query1 = context.Categories.ToList();
                //
                foreach (Category cate in query1)
                {
                    //có được category theo id
                    //Category cate = await TakeCategory(id);
                    //
                    ViewCategories vc = new ViewCategories();
                    vc.Id = cate.Id;
                    vc.Name = cate.Name;
                    //
                    listCate.Add(vc);
                }
                 result = new CategoryResult(listCate);
            }
            return result;

        }

        private async Task<Category> TakeCategory(int id)
        {
            Category cate = await Get_By_Id(id);
            return cate;
        }

    }



   
}
