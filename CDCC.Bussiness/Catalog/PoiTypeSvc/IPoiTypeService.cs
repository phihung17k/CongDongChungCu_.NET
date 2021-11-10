using CDCC.Bussiness.ViewModels.PoiType;
using System.Collections.Generic;

namespace CDCC.Bussiness.Catalog.PoiTypeSvc
{
    public interface IPoiTypeService
    {
        public List<PoiTypeViewModel> GetAll();
        public PoiTypeViewModel GetById(int id);
    }
}
