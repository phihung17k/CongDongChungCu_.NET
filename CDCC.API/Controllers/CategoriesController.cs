using CDCC.Bussiness.Catalog.CategorySvc;
using CDCC.Bussiness.ViewModels.Category;
using CDCC.Data.Common.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService cateService;
        public CategoriesController(ICategoryService cateService)
        {
            this.cateService = cateService;
        }
        [HttpGet]
        public IActionResult GetCategoriesOfStore([FromQuery] GetCategoryRequest request)
        {
            try
            {
                CategoryResult result = cateService.GetCategoriesByStore(request).Result;
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
                return Ok(cateService.GetById(id));
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
        public IActionResult InsertCategory([FromBody] InsertCategoryModel model)
        {
            try
            {
                //
                var result = cateService.InsertCategory(model);
                //
                if (result != null)
                {
                    return Created($"api/v1/[controller]/{result.Id}", result);
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


    }
}
