using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategotyController : BaseController
    {
        public CategotyController(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var category = await unit.CategoryRepository.GetAllAsync();
                if (category is null)
                    return BadRequest(new ResponseAPI(400,"No categories Founded"));
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }

        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            try
            {
                var category=await unit.CategoryRepository.GetByIdAsync(id);
                if (category is null)
                    return BadRequest(new ResponseAPI(400,$"not found category id={id}"));
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult>AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = mapper.Map<Category>(categoryDTO);
                    await unit.CategoryRepository.AddAsync(category);
                    return Ok(new ResponseAPI(200,"Item has been Added "));
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = mapper.Map<Category>(updateCategoryDTO);
                    await unit.CategoryRepository.UpdateAsync(category);

                    return Ok(new ResponseAPI(200, "Item has been Updated "));
                }
                else
                {
                   return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult>DeleteCatergory(int id)
        {
            try
            {
             
                await unit.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Item has been Deleted "));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
