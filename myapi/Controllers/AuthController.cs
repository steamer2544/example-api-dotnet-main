using Microsoft.AspNetCore.Mvc;
using myapi.DTOs.Requests;
using myapi.Services.Interfaces;

namespace myapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

       

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var result = await _service.Login(req);
            if (result == null) return Unauthorized();

            return Ok(result);
        }
    }
}
