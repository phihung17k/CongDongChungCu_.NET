//using CDCC.Bussiness.ViewModels.Category;
using CDCC.Bussiness.ViewModels.Category;
using CDCC.Data.Common.Categories;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository.CategoryRepository;
using CDCC.Data.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.CategorySvc
{
    public class CategoryService : ICategoryService
    {
        private ICateRepository repository;
        public CategoryService(ICateRepository repository)
        {
            this.repository = repository;
        }
        public List<ViewCategories> GetAll()
        {
            try
            {
                List<ViewCategories> cateList = new List<ViewCategories>();
                List<Category> result = repository.GetAll().ToList();
                if (result.Count == 0) throw new NullReferenceException("Not found");
                result.ForEach(x =>
                {
                    ViewCategories cate = new ViewCategories()
                    {
                        Id = x.Id,
                        Name = x.Name,
                    };
                    cateList.Add(cate);
                });
                return cateList;
            } catch (Exception)
            {
                throw;
            }
        }

        public ViewCategories GetById(int id)
        {
            try
            {
                Category cate = repository.Get(id);
                return new ViewCategories()
                {
                    Id = cate.Id,
                    Name = cate.Name
                };
            } catch (Exception)
            {
                throw;
            }
        }

        //
        public Task<CategoryResult> GetCategoriesByStore(GetCategoryRequest request)
        {
            return repository.GetCategoryOfStore(request);
        }

        //
        public ViewCategories InsertCategory(InsertCategoryModel request)
        {
            ViewCategories vc = new ViewCategories();
            Category cate = new Category();
            cate.Name = request.name;

            //trả result là kh còn dính task nữa
            int result = repository.Insert_(cate).Result;
            // return data
            if(result > 0)
            {
                //get data mới tạo ra
                Category cateReturn =  repository.Get_By_Id(result).Result;

                //set view model
                vc.Id = cateReturn.Id;
                vc.Name = cateReturn.Name;
                return vc;
            }
            return null;
        }

    }
}
