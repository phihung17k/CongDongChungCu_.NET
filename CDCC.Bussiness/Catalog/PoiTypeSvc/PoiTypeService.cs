using CDCC.Bussiness.ViewModels.PoiType;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDCC.Bussiness.Catalog.PoiTypeSvc
{
    public class PoiTypeService : IPoiTypeService
    {
        private IRepository<Poitype> repository;
        public PoiTypeService(IRepository<Poitype> repository)
        {
            this.repository = repository;
        }
        public List<PoiTypeViewModel> GetAll()
        {
            try
            {
                List<PoiTypeViewModel> poiTypeList = new List<PoiTypeViewModel>();
                List<Poitype> result = repository.GetAll().ToList();
                if (result.Count == 0) throw new NullReferenceException("Not found");
                result.ForEach(x =>
                {
                    PoiTypeViewModel cate = new PoiTypeViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                    };
                    poiTypeList.Add(cate);
                });
                return poiTypeList;
            } catch (Exception)
            {
                throw;
            }
        }

        public PoiTypeViewModel GetById(int id)
        {
            try
            {
                Poitype type = repository.Get(id);
                return new PoiTypeViewModel()
                {
                    Id = type.Id,
                    Name = type.Name
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
