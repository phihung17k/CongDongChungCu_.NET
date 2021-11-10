using CDCC.Bussiness.ViewModels.News;
using CDCC.Data.Common;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using System;
using System.Collections.Generic;

namespace CDCC.Bussiness.Catalog.NewsSvc
{
    public class NewsService : INewsService
    {
        private INewsRepository repository;
        public NewsService(INewsRepository repository)
        {
            this.repository = repository;
        }


        //public PagingResult<NewsViewModel> GetAll(PagingRequest request)
        //{
        //    PagingResult<News> pagingResult = repository.GetAllPaging(request);
        //    List<NewsViewModel> newsList = new List<NewsViewModel>();
        //    pagingResult.items.ForEach(x =>
        //    {
        //        NewsViewModel news = new NewsViewModel()
        //        {
        //            Id = x.Id,
        //            Content = x.Content,
        //            CreatedDate = x.CreatedDate,
        //            Status = x.Status,
        //            ApartmentId = x.ApartmentId
        //        };
        //        newsList.Add(news);
        //    });
        //    return new PagingResult<NewsViewModel>
        //        (newsList, pagingResult.TotalCount, pagingResult.CurrentPage, pagingResult.PageSize);
        //}

        public PagingResult<NewsViewModel> GetByCondition(GetNewsPagingRequest request)
        {
            try
            {
                PagingResult<News> pagingResult = repository.GetByCondition(request);
                List<NewsViewModel> newsList = new List<NewsViewModel>();
                pagingResult.items.ForEach(x =>
                {
                    NewsViewModel news = new NewsViewModel()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Content = x.Content,
                        CreatedDate = x.CreatedDate,
                        Status = (bool)x.Status,
                        ApartmentId = x.ApartmentId
                    };
                    newsList.Add(news);
                });
                return new PagingResult<NewsViewModel>
                    (newsList, pagingResult.TotalCount, pagingResult.CurrentPage, pagingResult.PageSize);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NewsViewModel GetById(int id)
        {
            try
            {
                News news = repository.Get(id);
                return new NewsViewModel()
                {
                    Id = news.Id,
                    Title = news.Title,
                    Content = news.Content,
                    CreatedDate = news.CreatedDate,
                    Status = (bool)news.Status,
                    ApartmentId = news.ApartmentId
                };
            } catch (Exception)
            {
                throw;
            }             
        }

        public int Insert(NewsInsertModel model)
        {
            try
            {
                News news = new News()
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedDate = DateTime.Now,
                    Status = true,
                    ApartmentId = model.ApartmentId
                };
                return repository.InsertExample(news);
            } catch (Exception)
            {
                throw;
            }
        }

        public bool Update(NewsUpdateModel model)
        {
            try
            {
                News news = repository.Get(model.Id);
                if (news != null)
                {
                    if (model.Title != null)
                    {
                        news.Title = model.Title;
                    }
                    if (model.Content != null)
                    {
                        news.Content = model.Content;
                    }
                    news.Status = model.Status;
                    return repository.Update(news);
                }
                return false;
            } catch (Exception)
            {
                throw;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                News news = repository.Get(id);
                return repository.Delete(id);
            } catch (Exception)
            {
                throw;
            }
        }
        //public Task<PagedResult<News>> GetAll(PagingRequestBase request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
