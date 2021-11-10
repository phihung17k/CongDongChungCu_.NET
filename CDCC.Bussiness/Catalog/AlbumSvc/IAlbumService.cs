using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels.Album;
using CDCC.Data.Common;

namespace CDCC.Bussiness.Catalog.AlbumSvc
{
    public interface IAlbumService
    {
        public PagingResult<AlbumViewModel> GetAll(PagingRequest request);
        public AlbumViewModel GetById(int id);
        public int Insert(AlbumInsertModel model);
        public bool Update(AlbumUpdateModel model);
        public bool Delete(int id);
    }
}
