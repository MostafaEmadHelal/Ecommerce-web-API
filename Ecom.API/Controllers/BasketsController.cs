using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class BasketsController : BaseController
    {
        public BasketsController(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }
        [HttpGet("get-basket-item/{id}")]
        public async Task<IActionResult>Get(string id)
        {
            var result=await unit.CustomerBasketRepository.GetBasketAsync(id);
            if (result is null) 
            {
                return Ok(new CustomerBasket());
            }
            return Ok(result);
        }
        [HttpPost("update-basket")]
        public async Task<IActionResult>Add(CustomerBasket customerBasket)
        {
            var result = await unit.CustomerBasketRepository.UpdateBasketAsync(customerBasket);
            return Ok(result);
        }
        [HttpDelete("delete-basket-item/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await unit.CustomerBasketRepository.DeleteBasketAsync(id);
            return result ? Ok(new ResponseAPI(200, "Item Deleted")) : BadRequest(new ResponseAPI(400));
        }
    }
}
