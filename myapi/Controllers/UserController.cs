using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myapi.DTOs.Requests;
using myapi.Services.Interfaces;

namespace myapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _service;

        public UserController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            var result = await _service.Register(req);
            if (result == null) return BadRequest("User exists");

            return Ok(result);
        }
    }
}
