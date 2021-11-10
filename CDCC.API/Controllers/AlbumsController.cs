using CDCC.Bussiness.Catalog.AlbumSvc;
using CDCC.Bussiness.ViewModels.Album;
using CDCC.Data.Common;
using CDCC.Util.ErrorResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Net;

namespace CDCC.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService albumService;
        public AlbumsController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PagingRequest request)
        {
            try
            {
                PagingResult<AlbumViewModel> result = albumService.GetAll(request);
                return Ok(result);
            }
            catch (NullReferenceException e)
            {
                return NotFound(new Error(HttpStatusCode.NotFound, e.Message));
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
                AlbumViewModel album = albumService.GetById(id);
                return Ok(album);
            }
            catch (NullReferenceException e)
            {
                return NotFound(new { code = 404, message = e.Message }
                    );
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] AlbumInsertModel model)
        {
            try
            {
                int id = albumService.Insert(model);
                AlbumViewModel album = albumService.GetById(id);
                return Created($"api/v1/[controller]/{id}", album);
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
        public IActionResult Update(int id, [FromBody] AlbumUpdateModel model)
        {
            try
            {
                model.Id = id;
                albumService.Update(model);
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
                albumService.Delete(id);
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
