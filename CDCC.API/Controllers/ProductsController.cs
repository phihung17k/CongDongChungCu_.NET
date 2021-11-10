using CDCC.Bussiness.Catalog.Products;
using CDCC.Bussiness.Common;
using CDCC.Bussiness.ViewModels;
using CDCC.Data.Common;
using CDCC.Data.Common.Products;
using CDCC.Data.ViewModels.Products;
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
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        
        public ProductsController(IProductService productService)
        {
            _productService = productService;
           
           
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult GetProductByStoreId([FromQuery]GetProductPagingRequest request)
        {
            try
            {
                PagingResult<ViewProducts> pageResult = _productService.Get_Product_By_StoreId(request);
                if (pageResult.TotalCount > 0 )
                {
                    return Ok(pageResult);
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



        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
               
                ViewProducts view = await _productService.GetProductById(id);

                var JsonProduct = JsonConvert.SerializeObject(view);

                if (JsonProduct == null)
                {
                    return NotFound();
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


        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromBody] InsertProductModel model)
        {
            try
            {
                ViewProducts result = await _productService.InsertProduct(model);
                //true
                if (result != null)
                {
                    return Created($"api/v1/[controller]/{result.Id}", result);
                }
                else
                {
                    //
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

        // PUT api/<ProductController>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductModel model)
        {
            try
            {
                model.Id = id;
                Boolean result = await _productService.UpdateProduct(model);
                //true
                if (result)
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

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            try
            {
                int num = int.Parse(id);
                //
                Boolean result = await _productService.DeleteProduct(num);
                //true
                if (result)
                {
                    return Ok();
                }
                else
                {
                    //
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


        //// GET: api/<ProductController>
        //[HttpGet("all")]
        //public async Task<IActionResult> GetAllProduct()
        //{
        //    //
        //    PagedResult<ViewProduct> pageResult = await _productService.GetAllProduct();
        //    //
        //    var JsonPageResult = JsonConvert.SerializeObject(pageResult);

        //    if (JsonPageResult == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(JsonPageResult);
        //}



        



        //// GET api/<ProductController>/5
        //[HttpGet("indexPage")]
        //public async Task<IActionResult> GetProductByIndexPage([FromQuery]PagingRequestBase condition)
        //{

        //    PagedResult<ViewProduct> pageResult = await _productService.GetProductPaging(condition);

        //    var JsonProducts = JsonConvert.SerializeObject(pageResult);

        //    if (JsonProducts == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(JsonProducts);
        //}



    }
}
