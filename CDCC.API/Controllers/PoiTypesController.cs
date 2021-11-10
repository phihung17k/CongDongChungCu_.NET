using CDCC.Bussiness.Catalog.PoiTypeSvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PoiTypesController : ControllerBase
    {
        private readonly IPoiTypeService poiTypeService;
        public PoiTypesController(IPoiTypeService poiTypeService)
        {
            this.poiTypeService = poiTypeService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = poiTypeService.GetAll();
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
                return Ok(poiTypeService.GetById(id));
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
