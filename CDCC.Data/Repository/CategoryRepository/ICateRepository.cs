using CDCC.Data.Common;
using CDCC.Data.Common.Categories;
using CDCC.Data.Models.DB;
using CDCC.Data.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Repository.CategoryRepository
{
    public interface ICateRepository : IRepository<Category>
    {
        //get category theo store
      Task<CategoryResult> GetCategoryOfStore(GetCategoryRequest request);
    }
}
