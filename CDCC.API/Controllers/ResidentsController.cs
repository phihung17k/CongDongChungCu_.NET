using CDCC.Bussiness.Catalog.Residents;
using CDCC.Bussiness.JWT;
using CDCC.Bussiness.ViewModels.Resident;
using CDCC.Data.CustomException;
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
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentService residentService;

        public ResidentsController(IResidentService residentService)
        {
            this.residentService = residentService;
        }

        //[Authorize(Policy = "Admin")]
        [HttpGet]
        public ActionResult<List<ResidentViewModel>> GetAllResident([FromHeader(Name = "Security-Data")] string jsonString,
            [FromQuery(Name = "status")] bool? status)
        {
            //"Security-Data" : {"UserId": 28}
            ResidentHeaderInsertModel insertModel =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<ResidentHeaderInsertModel>(jsonString);
            //if (insertModel.UserId.HasValue)
            //{
            //    return Ok(residentService.GetAll(insertModel.UserId, status));
            //}
            if (insertModel.UserId.HasValue)
            {
                if (status.HasValue)
                {
                    return Ok(residentService.GetAll(insertModel.UserId, status));
                }
                else
                {
                    return Ok(residentService.GetAll(insertModel.UserId));
                }
            }
            return BadRequest("Not found header");
        }

        //after login, user choose a existed resident on screen
        [HttpGet("{id}")]
        public async Task<ActionResult> GetResident(string id)
        {
            ResidentViewResult result = null;
            try
            {
                int tempID = Convert.ToInt32(id);
                ResidentViewModel temp = await residentService.Get(tempID);
                result = new ResidentViewResult()
                {
                    Id = temp.Id,
                    IsAdmin = temp.IsAdmin
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new ResidentHeaderModel()
                {
                    UserId = temp.UserId,
                    BuildingId = temp.BuildingId,
                    ApartmentId = temp.ApartmentId
                });
                string jwtToken = JWTGeneration.GenerateJSONWebTokenAsync(
                        temp.UserId,
                        temp.Id,
                        temp.IsAdmin);
                HttpContext.Response.Headers["Authorization"] = jwtToken;
                HttpContext.Response.Headers.Add("Security-Data", json);
            }
            catch (Exception e)
            {
                if(e is ResidentIDNotFoundException)
                {
                    return NotFound(e.Message);
                }
            }
            return Ok(result);
        }

        //create resident, client send header security-data contains user id
        //client send apartment id and building id in from body as normal (not header)
        //this function is called by resident => isAdmin = false, status = true
        [HttpPost]
        public async Task<ActionResult> InsertResident([FromBody] ResidentRequestModel residentRequest,
            [FromHeader(Name = "Security-Data")] string jsonString)
        {
            ResidentViewResult result = null;
            try
            {
                ResidentHeaderInsertModel insertModel =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<ResidentHeaderInsertModel>(jsonString);

                ResidentViewModel insertedResident = await residentService.Insert(new ResidentInsertModel()
                {
                    IsAdmin = false,
                    Status = true,
                    UserId = (int)insertModel.UserId,
                    BuildingId = residentRequest.BuildingId,
                    ApartmentId = residentRequest.ApartmentId
                });
                result = new ResidentViewResult()
                {
                    Id = insertedResident.Id,
                    IsAdmin = insertedResident.IsAdmin
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new ResidentHeaderModel()
                {
                    UserId = insertedResident.UserId,
                    BuildingId = insertedResident.BuildingId,
                    ApartmentId = insertedResident.ApartmentId
                });
                string jwtToken = JWTGeneration.GenerateJSONWebTokenAsync(
                    insertedResident.UserId,
                    insertedResident.Id,
                    insertedResident.IsAdmin);
                HttpContext.Response.Headers["Authorization"] = jwtToken;
                HttpContext.Response.Headers.Add("Security-Data", json);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created($"api/v1/residents/{result.Id}", result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateResident([FromBody] ResidentViewModel resident)
        {
            bool result = false;
            try
            {
                result = await residentService.Update(resident);
                return Ok(result);
            }
            catch (Exception e)
            {
                if(e is ResidentIDNotFoundException)
                {
                    return NotFound(e.Message);
                }
            }
            return BadRequest("sssssssssss");
        }

        //user click delete, set status = false
        //[HttpPatch("{id}")]
        //public async Task<ActionResult> UpdateStatusResident(int id)
        //{
        //    bool result = false;
        //    try
        //    {
        //        result = await residentService.UpdateStatus(id);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResident(int id)
        {
            try
            {
                await residentService.Delete(id);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
    }
}
