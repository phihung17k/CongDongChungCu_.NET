using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels.Image;
using CDCC.Data.Common;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDCC.Bussiness.Catalog.ImageSvc
{
    public class ImageService : IImageService
    {
        private IRepository<Image> repository;
        public ImageService(IRepository<Image> repository)
        {
            this.repository = repository;
        }

        public PagingResult<ImageViewModel> GetAll(PagingRequest request)
        {
            try
            {
                PagingResult<Image> pagingResult = repository.GetAllPaging(request);
                List<ImageViewModel> imageList = new List<ImageViewModel>();
                pagingResult.items.ForEach(x =>
                {
                    ImageViewModel news = new ImageViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Url = x.Url,
                        AlbumId = x.AlbumId
                    };
                    imageList.Add(news);
                });
                return new PagingResult<ImageViewModel>
                        (imageList, pagingResult.TotalCount, pagingResult.CurrentPage, pagingResult.PageSize);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ImageViewModel GetById(int id)
        {
            try
            {
                Image image = repository.Get(id);
                return new ImageViewModel()
                {
                    Id = image.Id,
                    Name = image.Name,
                    Url = image.Url,
                    AlbumId = image.AlbumId
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(ImageInsertModel model)
        {
            try
            {
                Image image = new Image()
                {
                    Name = model.Name,
                    Url = model.Url,
                    AlbumId = model.AlbumId
                };
                return repository.InsertExample(image);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(ImageUpdateModel model)
        {
            try
            {
                Image image = repository.Get(model.Id);
                if (image != null)
                {
                    image.Name = model.Name;
                    image.Url = image.Url;
                    return repository.Update(image);
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
                Image image = repository.Get(id);
                return repository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
