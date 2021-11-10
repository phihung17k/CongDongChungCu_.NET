using CDCC.Bussiness.Catalog.Comments;
using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common.Comments;
using CDCC.Data.ViewModels.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommentsController : ControllerBase
    {

        ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }


   

        // GET api/<CommentController>/5
        [HttpGet]
        public IActionResult GetCommentByPostId([FromQuery] GetCommentPagingRequest request)
        {
            try
            {
                //
                var kq = _commentService.GetCommentOfPost(request);
                if (kq.TotalCount > 0 )
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


        // GET: api/<CommentController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentId(int id)
        {
            try
            {
                ViewComments view = await _commentService.GetCommentById(1);

                var JsonProduct = JsonConvert.SerializeObject(view);

                if (JsonProduct == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(JsonProduct);
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

        // POST api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> InsertComment([FromBody] InsertCommentModel model)
        {

            try
            {
                //
                var result = await _commentService.InsertComment(model);
                //
                if (result != null)
                {
                    return Created($"api/v1/[controller]/{result.Id}", result);
                }else
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

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentModel model)
        {
            try
            {
                Boolean check = false;
                //
                model.CommentId = id;
                check = await _commentService.UpdateComment(model);
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

        // DELETE api/<CommentController>/5
        [HttpDelete()]
        public async Task<IActionResult> DeleteComment([FromQuery] int CommentId, [FromQuery] int OwnerCommentId)
        {
            try
            {
                Boolean check = false;
                //
                check = await _commentService.DeleteComment(CommentId, OwnerCommentId);
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
    }
}
