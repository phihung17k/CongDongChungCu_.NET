using CDCC.Bussiness.ViewModels.Category;
using CDCC.Data.Common.Categories;
using CDCC.Data.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.CategorySvc
{
    public interface ICategoryService
    {
        public List<ViewCategories> GetAll();
        public ViewCategories GetById(int id);

        //insert
        public ViewCategories InsertCategory(InsertCategoryModel request);

        public Task<CategoryResult> GetCategoriesByStore(GetCategoryRequest request);
    }
}
