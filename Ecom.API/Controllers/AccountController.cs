using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result=await unit.Auth.RegisterAsync(registerDTO);
            if (result != "Done")
            {
                return BadRequest(new ResponseAPI(400, result));
            }
            return Ok(new ResponseAPI(200,result));
        }
        [HttpPost("Login")]
        public async Task<IActionResult>Login(LoginDTO loginDTO)
        {
            var result=await unit.Auth.LoginAsync(loginDTO);
            if (result.StartsWith("Please"))
            {
                return BadRequest(new ResponseAPI(400,result));
            }
            Response.Cookies.Append("token", result, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Domain ="localhost",
                Expires=DateTime.Now.AddDays(1),
                IsEssential = true,
                SameSite=SameSiteMode.Strict
            });
            return Ok(new ResponseAPI(200));
        }
        [HttpPost("active-account")]
        public async Task<IActionResult>Active(ActiveAccountDTO activeAccountDTO)
        {
            var result = await unit.Auth.ActiveAccount(activeAccountDTO);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(400));
        }
        [HttpGet("send-email-forget-password")]
        public async Task<IActionResult> Forget(string email)
        {
            var result=await unit.Auth.SendEmailForForgetPassword(email);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(400));
        }
    }
}
