using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels.Image;
using CDCC.Data.Common;

namespace CDCC.Bussiness.Catalog.ImageSvc
{
    public interface IImageService
    {
        public PagingResult<ImageViewModel> GetAll(PagingRequest request);
        public ImageViewModel GetById(int id);
        public int Insert(ImageInsertModel model);
        public bool Update(ImageUpdateModel model);
        public bool Delete(int id);
    }
}
