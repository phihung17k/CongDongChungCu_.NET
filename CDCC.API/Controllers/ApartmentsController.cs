
using CDCC.Bussiness.Catalog.Apartments;
using CDCC.Bussiness.ViewModels.Apartment;
using CDCC.Data.CustomException;
using CDCC.Data.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            this.apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApartmentViewModel>>> GetAllApartment()
        {
            return Ok(await apartmentService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApartmentViewModel>> GetApartment(string id)
        {
            ApartmentViewModel result = null;
            try
            {
                int tempID = Convert.ToInt32(id);
                result = await apartmentService.Get(tempID);
            }
            catch (Exception e)
            {
                if (e is ApartmentIDNotFoundException)
                {
                    return NotFound(e.Message);
                }
            }
            return Ok(result);
        }

        [Authorize(Policy = "SystemAdmin")]
        [HttpPost]
        public async Task<ActionResult> InsertApartment([FromBody] ApartmentInsertModel apartment)
        {
            ApartmentViewModel result = null;
            try
            {
                result = await apartmentService.Insert(apartment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created($"api/v1/apartments/{result.Id}", result);
        }

        [Authorize(Policy = "SystemAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateApartment([FromBody] ApartmentViewModel apartment)
        {
            bool result = false;
            try
            {
                result = await apartmentService.Update(apartment);
            }
            catch (Exception e)
            {
                if (e is ResidentIDNotFoundException)
                {
                    return NotFound(e.Message);
                }
            }
            return Ok(result);
        }

        //user click delete, set status = false
        //[HttpPatch("{id}")]
        //public async Task<ActionResult> UpdateStatusApartment(int id)
        //{
        //    bool result = false;
        //    try
        //    {
        //        result = await apartmentService.UpdateStatus(id);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is ResidentIDNotFoundException)
        //        {
        //            return NotFound(e.Message);
        //        }
        //        if (e is ResidentDeletedException)
        //        {
        //            return BadRequest(e.Message);
        //        }
        //    }
        //    return Ok(result);
        //}

        [Authorize(Policy = "SystemAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApartment(int id)
        {
            try
            {
                await apartmentService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
    }
}
