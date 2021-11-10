using CDCC.Bussiness.Catalog.ImageSvc;
using CDCC.Bussiness.ViewModels.Image;
using CDCC.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;
        public ImagesController(IImageService newsService)
        {
            this.imageService = newsService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PagingRequest request)
        {
            try
            {
                PagingResult<ImageViewModel> result = imageService.GetAll(request);
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

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(imageService.GetById(id));
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
        public IActionResult Insert([FromBody] ImageInsertModel model)
        {
            try
            {
                int id = imageService.Insert(model);
                ImageViewModel imageModel = new ImageViewModel()
                {
                    Id = id,
                    Name = model.Name,
                    Url = model.Url,
                    AlbumId = model.AlbumId
                };
                return Created($"api/v1/[controller]/{id}", imageModel);
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
        public IActionResult Update(int id, [FromBody] ImageUpdateModel model)
        {
            try
            {
                model.Id = id;
                imageService.Update(model);
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
                imageService.Delete(id);
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
