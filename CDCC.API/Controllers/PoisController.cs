using CDCC.Bussiness.Catalog.PoiSvc;
using CDCC.Bussiness.ViewModels.Poi;
using CDCC.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PoisController : ControllerBase
    {
        private readonly IPoiService poiService;
        public PoisController(IPoiService poiService)
        {
            this.poiService = poiService;
        }

        [HttpGet]
        public IActionResult GetByCondition([FromHeader(Name = "Security-Data")] string jsonString, [FromQuery] GetPoiPagingRequest request)
        {
            try
            {
                if (jsonString != null)
                {
                    PoiHeaderGetModel poiheader =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<PoiHeaderGetModel>(jsonString);
                    if (poiheader.ApartmentId.HasValue)
                    {
                        request.ApartmentId = poiheader.ApartmentId;
                    }
                }
                PagingResult<PoiViewModel> result = poiService.GetByCondition(request);
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
                return Ok(poiService.GetById(id));
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
        public IActionResult Insert([FromHeader(Name = "Security-Data")] string jsonString, [FromBody] PoiInsertModel model)
        {
            try
            {
                PoiHeaderInsertModel poiheader =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<PoiHeaderInsertModel>(jsonString);
                model.ApartmentId = poiheader.ApartmentId;
                int id = poiService.Insert(model);
                PoiViewModel poiModel = poiService.GetById(id);
                return Created($"api/v1/[controller]/{id}", poiModel);
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
        public IActionResult Update(int id, [FromBody] PoiUpdateModel model)
        {
            try
            {
                model.Id = id;
                poiService.Update(model);
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
                poiService.Delete(id);
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
