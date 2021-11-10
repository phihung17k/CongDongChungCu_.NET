using CDCC.Bussiness.Catalog.Stores;
using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StoresController : ControllerBase
    {
        IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: api/<StoreController>
        [HttpGet]
        public IActionResult GetByCondition([FromHeader(Name = "Security-Data")] string jsonString, [FromQuery] GetStorePagingRequest request)
        {
            try
            {
                //trường hợp nằm ở  header
                if (jsonString != null)
                {
                    RecieveHeader HeaderModel =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<RecieveHeader>(jsonString);

                    if (HeaderModel.ApartmentId.HasValue)
                    {
                        request.ApartmentId = (int)HeaderModel.ApartmentId;

                        PagingResult<ViewStores> result = _storeService.GetByCondition(request);
                        if (result.TotalCount > 0)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
               
             //trường hợp nằm ở  query
             if (request.ApartmentId != null)
                    {

                        PagingResult<ViewStores> result = _storeService.GetByCondition(request);
                        if (result.TotalCount > 0)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }

                return BadRequest("Missing apartment id");

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


        // GET: api/<StoreController>
        [HttpGet("condition")]
        public  IActionResult GetByCondition([FromQuery] GetStore request)
        {
            try
            {

                ViewStores result = _storeService.GetStoreById_ResidentId(request);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
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


        // POST api/<StoreController>
        [HttpPost]
        public async Task<IActionResult> InsertStore([FromBody] InsertStoreModel model)
        {
            try
            {
                ViewStores result = await _storeService.InsertStore(model);
                //
                if (result != null)
                {
                    return Created($"api/v1/[controller]/{result.StoreId}", result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }

        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStoreById(int id, [FromBody] UpdateStoreModel model)
        {
            try
            {
                //
                Boolean check = false;
                //
                model.StoreId = id;
                check = await _storeService.UpdateStore(model);
                //
                if (check)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
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

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreById (int id)
        {
            try
            {
                //
                Boolean check = false;
                //
                check = await _storeService.DeleteStore(id);
                //
                if (check)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
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

        //// GET api/<StoreController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
    }
}
