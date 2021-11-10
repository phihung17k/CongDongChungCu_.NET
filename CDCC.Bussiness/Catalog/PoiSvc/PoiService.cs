using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels.Poi;
using CDCC.Data.Common;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using System;
using System.Collections.Generic;

namespace CDCC.Bussiness.Catalog.PoiSvc
{
    public class PoiService : IPoiService
    {
        private IPoiRepository repository;
        public PoiService(IPoiRepository repository)
        {
            this.repository = repository;
        }

        public PagingResult<PoiViewModel> GetByCondition(GetPoiPagingRequest request)
        {
            try
            {
                PagingResult<Poi> pagingResult = repository.GetByCondition(request);
                List<PoiViewModel> poiList = new List<PoiViewModel>();
                pagingResult.items.ForEach(x =>
                {
                    PoiViewModel poi = new PoiViewModel()
                    {
                        Id = x.Id,
                        Address = x.Address,
                        Name = x.Name,
                        Phone = x.Phone,
                        Status = (bool)x.Status,
                        ApartmentId = x.ApartmentId,
                        PoitypeId = x.PoitypeId
                    };
                    poiList.Add(poi);
                });
                return new PagingResult<PoiViewModel>
                    (poiList, pagingResult.TotalCount, pagingResult.CurrentPage, pagingResult.PageSize);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PoiViewModel GetById(int id)
        {
            try
            {
                Poi poi = repository.Get(id);
                return new PoiViewModel()
                {
                    Id = poi.Id,
                    Address = poi.Address,
                    Name = poi.Name,
                    Phone = poi.Phone,
                    Status = (bool)poi.Status,
                    ApartmentId = poi.ApartmentId,
                    PoitypeId = poi.PoitypeId
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(PoiInsertModel model)
        {
            try
            {
                Poi poi = new Poi()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Phone = model.Phone,
                    Status = true,
                    PoitypeId = model.PoitypeId,
                    ApartmentId = model.ApartmentId
                };
                return repository.InsertExample(poi);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(PoiUpdateModel model)
        {
            try
            {
                Poi poi = repository.Get(model.Id);
                if (poi != null)
                {
                    if (model.Name != null)
                    {
                        poi.Name = model.Name;
                    }
                    if (model.Phone != null)
                    {
                        poi.Phone = model.Phone;
                    }
                    if (model.Address != null)
                    {
                        poi.Address = model.Address;
                    }
                    poi.Status = model.Status;
                    if (model.PoitypeId != 0)
                    {
                        poi.PoitypeId = model.PoitypeId;
                    }
                    return repository.Update(poi);
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                return repository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
