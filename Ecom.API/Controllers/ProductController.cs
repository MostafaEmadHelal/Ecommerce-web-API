using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> Get([FromQuery]ProductParam productParam)
        {
            try
            {

                var products = await unit.ProductRepository.GetAllAsync(productParam);
                if (products.TotalCount==0 )
                    return BadRequest(new ResponseAPI(400, "Products not found"));
                //var result = mapper.Map<List<ProductDTO>>(products);
                //var totalCount = unit.ProductRepository.CountAsync();
                return Ok(new Pagination<ProductDTO>(productParam.pageNumber,productParam.pageSize, products.TotalCount, (IReadOnlyList<ProductDTO>)products.products));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            try
            {
                var product = await unit.ProductRepository.GetByIdAsync(id,x=>x.Category,x=>x.Photos);
                if (product is null)
                    return BadRequest(new ResponseAPI(400, $"Product Not found at Id {id}"));
                var result=mapper.Map<ProductDTO>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add-Product")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> AddPorduct(AddProductDTO product)
        {
            try
            {
                await unit.ProductRepository.AddAsync(product);
                return Ok(new ResponseAPI(200,"Product Added Successfully"));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message + ex.InnerException?.Message));
            }
        }
        [HttpPut("Update-Product")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDTO updateProductDTO)
        {
            try
            {
                var result = await unit.ProductRepository.UpdateAsync(updateProductDTO);

                if (!result)
                    return NotFound(new ResponseAPI(404, "Product not found or update failed."));

                return Ok(new ResponseAPI(200, "Product Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, $"Error: {ex.Message} {ex.InnerException?.Message}"));
            }
        }
        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product=await unit.ProductRepository.GetByIdAsync(id,x=>x.Category,x=>x.Photos);
                if (product is null) return BadRequest(new ResponseAPI(404, "Product Not Found"));
                await unit.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product Deleted Successfully"));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message ));
            }
        }

    }
}
