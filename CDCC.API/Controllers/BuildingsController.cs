using CDCC.Bussiness.Catalog.Buildings;
using CDCC.Bussiness.ViewModels.Building;
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
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            this.buildingService = buildingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BuildingViewModel>>> GetAllBuilding()
        {
            return Ok(await buildingService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingViewModel>> GetBuilding(string id)
        {
            BuildingViewModel result = null;
            try
            {
                int tempID = Convert.ToInt32(id);
                result = await buildingService.Get(tempID);
            }
            catch (Exception e)
            {
                if (e is BuildingIDNotFoundException)
                {
                    return NotFound(e.Message);
                }
            }
            return Ok(result);
        }

        [Authorize(Policy = "SystemAdmin")]
        [HttpPost]
        public async Task<ActionResult> InsertBuilding([FromBody] BuildingInsertModel building)
        {
            BuildingViewModel result = null;
            try
            {
                result = await buildingService.Insert(building);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created($"api/v1/buildings/{result.Id}", result);
        }

        [Authorize(Policy = "SystemAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateBuilding([FromBody] BuildingViewModel building)
        {
            bool result = false;
            try
            {
                result = await buildingService.Update(building);
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
        //public async Task<ActionResult> UpdateStatusBuilding(int id)
        //{
        //    bool result = false;
        //    try
        //    {
        //        result = await buildingService.UpdateStatus(id);
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
        public async Task<ActionResult> DeleteBuilding(int id)
        {
            try
            {
                await buildingService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
    }
}
