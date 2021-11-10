using CDCC.Bussiness.ViewModels.News;
using CDCC.Data.Common;

namespace CDCC.Bussiness.Catalog.NewsSvc
{
    public interface INewsService
    {
        public PagingResult<NewsViewModel> GetByCondition(GetNewsPagingRequest request);
        //public PagingResult<NewsViewModel> GetAll(PagingRequest request);
        public NewsViewModel GetById(int id);
        public int Insert(NewsInsertModel model);
        public bool Update(NewsUpdateModel model);
        public bool Delete(int id);
    }
}
