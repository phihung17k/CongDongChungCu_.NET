using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels.Album;
using CDCC.Data.Common;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDCC.Bussiness.Catalog.AlbumSvc
{
    public class AlbumService : IAlbumService
    {
        private IRepository<Album> repository;
        public AlbumService(IRepository<Album> repository)
        {
            this.repository = repository;
        }

        public PagingResult<AlbumViewModel> GetAll(PagingRequest request)
        {
            try
            {
                PagingResult<Album> pagingResult = repository.GetAllPaging(request);
                List<AlbumViewModel> albumList = new List<AlbumViewModel>();
                pagingResult.items.ForEach(x =>
                {
                    AlbumViewModel news = new AlbumViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ExternalCode = x.ExternalCode,
                        OwnerId = x.OwnerId,
                        Type = x.Type,
                        Status = x.Status,
                        ApartmentId = x.ApartmentId
                    };
                    albumList.Add(news);
                });
                return new PagingResult<AlbumViewModel>
                    (albumList, pagingResult.TotalCount, pagingResult.CurrentPage, pagingResult.PageSize);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public AlbumViewModel GetById(int id)
        {
            try
            {
                Album album = repository.Get(id);
                return new AlbumViewModel()
                {
                    Id = album.Id,
                    Name = album.Name,
                    ExternalCode = album.ExternalCode,
                    OwnerId = album.OwnerId,
                    Type = album.Type,
                    Status = album.Status,
                    ApartmentId = album.ApartmentId
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(AlbumInsertModel model)
        {
            try
            {
                Album album = new Album()
                {
                    Name = model.Name,
                    ExternalCode = Guid.NewGuid(),
                    OwnerId = model.OwnerId,
                    Type = model.Type,
                    Status = model.Status,
                    ApartmentId = model.ApartmentId
                };
                return repository.InsertExample(album);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(AlbumUpdateModel model)
        {
            try
            {
                Album album = repository.Get(model.Id);
                if (album != null)
                {
                    album.Name = model.Name;
                    album.Status = model.Status;
                    album.OwnerId = model.OwnerId;
                    album.Type = model.Type;
                    return repository.Update(album);
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
