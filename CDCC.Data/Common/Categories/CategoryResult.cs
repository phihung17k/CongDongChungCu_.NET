using CDCC.Data.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.Common.Categories
{
    public class CategoryResult
    {
        public List<ViewCategories> items { get; set; }

        //construcotr
        public CategoryResult(List<ViewCategories> list)
        {
            this.items = list;
        }
    }
}
