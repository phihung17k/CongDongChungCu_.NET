
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CDCC.Bussiness.Catalog.Users;
using System;
using CDCC.Bussiness.ViewModels.User;
using System.Threading.Tasks;
using CDCC.Data.CustomException;
using Microsoft.AspNetCore.Authorization;
using CDCC.Data.Common;
using CDCC.Bussiness.JWT;

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Policy = "SystemAdmin")]
        public ActionResult GetAllUser([FromQuery] PagingRequest request)
        {
            try
            {
                if(request.currentPage <= 0)
                {
                    return BadRequest("Invalid request: current page must be more than zero");
                }
                PagingResult<UserViewModel> result = _userService.GetAll(request);
                return Ok(result);
            }
            catch(NullReferenceException ne)
            {
                return NotFound(ne.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal server exception");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(string id, [FromHeader(Name = "email")] string jsonString)
        {
            UserViewModel result = null;
            try
            {
                if(jsonString != null && jsonString.Length > 0)
                {
                    UserEmailModel userEmail =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<UserEmailModel>(jsonString);
                    if(userEmail != null)
                    {
                        UserViewModel checkedUser = await _userService.GetUserByEmail(userEmail.Email);
                        if (checkedUser == null)
                        {
                            return BadRequest("Email is not existed");
                        }
                        else
                        {
                            result = checkedUser;
                            String clientToken = JWTGeneration.GenerateJSONWebTokenAsync(result, result.IsSystemAdmin);
                            HttpContext.Response.Headers["Authorization"] = clientToken;
                        }
                    }
                } 
                else
                {
                    int tempID = Convert.ToInt32(id);
                    result = await _userService.Get(tempID);
                    String clientToken = JWTGeneration.GenerateJSONWebTokenAsync(result, result.IsSystemAdmin);
                    HttpContext.Response.Headers["Authorization"] = clientToken;
                }
            }
            catch (Exception e)
            {
                if (e is UserIDNotFoundException)
                {
                    return NotFound(e.Message);
                }
                else if (e is UserDeletedException)
                {
                    return BadRequest(e.Message);
                }
                else
                {
                    return BadRequest("Something is wrong");
                }
            }
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult> InsertUser([FromBody] UserInsertModel user)
        {
            UserViewModel result = null;
            try
            {
                if (user.Email == null || user.Email.Length == 0)
                {
                    return BadRequest("Request is rejected");
                }
                UserViewModel checkedUser = await _userService.GetUserByEmail(user.Email);
                if(checkedUser == null)
                {
                    //user is not existed

                    string username = user.Email.Split("@")[0];
                    UserInsertModel insertedUser = new UserInsertModel()
                    {
                        Username = username,
                        Password = user.Password,
                        Fullname = username,
                        Phone = "",
                        Email = user.Email,
                        IsSystemAdmin = false,
                        Status = true
                    };
                    result = await _userService.Insert(insertedUser);
                    String clientToken = JWTGeneration.GenerateJSONWebTokenAsync(result, false);
                    HttpContext.Response.Headers["Authorization"] = clientToken;
                }
                else
                {
                    return BadRequest("User is existed");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created($"api/v1/users/{result.Id}", result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateUser([FromBody] UserViewModel user)
        {
            bool result = false;
            try
            {
                result = await _userService.Update(user);
            }
            catch (Exception e)
            {
                if (e is UserIDNotFoundException)
                {
                    return NotFound(e.Message);
                }
            }
            return Ok(result);
        }

        //user click delete, set status = false
        //[HttpPatch("{id}")]
        //[Authorize(Policy = "SystemAdmin")]
        //public async Task<ActionResult> UpdateStatusUser(int id)
        //{
        //    bool result = false;
        //    try
        //    {
        //        result = await _userService.UpdateStatus(id);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is UserIDNotFoundException)
        //        {
        //            return NotFound(e.Message);
        //        }
        //        if (e is UserDeletedException)
        //        {
        //            return BadRequest(e.Message);
        //        }
        //    }
        //    return Ok(result);
        //}

        [HttpDelete("{id}")]
        [Authorize(Policy = "SystemAdmin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
    }
}
