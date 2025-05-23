using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork unit;
        protected readonly IMapper mapper;

        public BaseController(IUnitOfWork unit,IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
    }
}
