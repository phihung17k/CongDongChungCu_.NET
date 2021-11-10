using CDCC.Bussiness.Catalog.NewsSvc;
using CDCC.Bussiness.ViewModels.News;
using CDCC.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService newsService;
        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        [HttpGet]
        public IActionResult GetByCondition
            ([FromHeader(Name = "Security-Data")] string jsonString, [FromQuery] GetNewsPagingRequest request)
        {
            try
            {
                if (jsonString != null)
                {
                    NewsHeaderGetModel newsheader =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<NewsHeaderGetModel>(jsonString);
                    if (newsheader.ApartmentId.HasValue)
                    {
                        request.ApartmentId = newsheader.ApartmentId;
                    }
                }
                PagingResult<NewsViewModel> result = newsService.GetByCondition(request);
                return Ok(result);
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }
        }

        //[HttpGet("getAll")]
        //public IActionResult GetAll([FromQuery]PagingRequest request)
        //{
        //    PagingResult<NewsViewModel> result = newsService.GetAll(request);
        //    if (result.items != null)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest();
        //}

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(newsService.GetById(id));
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult Insert
            ([FromHeader(Name = "Security-Data")] string jsonString, [FromBody] NewsInsertModel model)
        {
            try
            {
                NewsHeaderInsertModel newsheader =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<NewsHeaderInsertModel>(jsonString);
                model.ApartmentId = newsheader.ApartmentId;
                int id = newsService.Insert(model);
                NewsViewModel newModel = newsService.GetById(id);
                return Created($"api/v1/[controller]/{id}", newModel);
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "SystemAdmin")]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(int id, [FromBody] NewsUpdateModel model)
        {
            try
            {
                model.Id = id;
                newsService.Update(model);
                return Ok();
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "SystemAdmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                newsService.Delete(id);
                return Ok();
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }
        }
    }
}
