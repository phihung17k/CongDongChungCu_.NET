using CDCC.Bussiness.Catalog.Posts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CDCC.Data.Common.Posts;
using Microsoft.Data.SqlClient;
using CDCC.Data.ViewModels.Posts;
using CDCC.Data.Common;
using CDCC.Bussiness.ViewModels;
using CDCC.Bussiness.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PostsController : ControllerBase
    {

        IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        

        // POST api/<PostController>
        [HttpGet]
        public IActionResult GetAllPostBelongToApartment_Paging([FromHeader(Name = "Security-Data")] string jsonString, [FromQuery] GetPostPagingRequest request)
        {
            try
            {
                if(jsonString != null) { 
                RecieveHeader HeaderModel =
                   Newtonsoft.Json.JsonConvert.DeserializeObject<RecieveHeader>(jsonString);

                    if (HeaderModel.ApartmentId.HasValue)
                    {
                        request.ApartmentId = (int)HeaderModel.ApartmentId;

                        //
                        PagingResult<ViewPosts> kq = _postService.GetAllPostBelongToApartment_Paging(request);

                        if (kq.TotalCount > 0)
                        {
                            return Ok(kq);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }

                if(request.ApartmentId > 0 )
                {
                    //
                    PagingResult<ViewPosts> kq = _postService.GetAllPostBelongToApartment_Paging(request);

                    if (kq.TotalCount > 0)
                    {
                        return Ok(kq);
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

        //GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostByPostId(int id)
        {
            try
            {
                //
                ViewPosts kq = await _postService.GetPostById(id);
                if (kq != null)
                {
                    return Ok(kq);
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


        // POST api/<PostController>
        [HttpPost]
        public async Task<IActionResult> InsertPost([FromBody] InsertPostModel model)
        {
            try
            {
                var kq = await _postService.InsertPost(model);
                //
                if (kq != null)
                {
                    return Created($"api/v1/[controller]/{kq.Id}", kq);
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

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostModel model)
        {
            try
            {
                //
                Boolean check = false;
                //
                model.Id = id;
                check = await _postService.UpdatePost(model);
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


        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(String id)
        {
            try
            {
                //
                int num = int.Parse(id);
                //
                Boolean check = false;
                //
                check = await _postService.DeletePost(num);

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


        ////GET: api/<PostController>
        //[HttpGet]
        //public async Task<IActionResult> GetPostBelongToApartment([FromQuery] String residentID)
        //{
        //    //
        //    int id = int.Parse(residentID);
        //    //
        //    Hashtable result = await _postService.GetPost_All_BelongToApartMent(id);
        //    //
        //    if (result == null)
        //    {
        //        return BadRequest();
        //    }

        //    //
        //    var JsonPosts = JsonConvert.SerializeObject(result);

        //    //
        //    return Ok(JsonPosts);

        //}

        

    }
}
