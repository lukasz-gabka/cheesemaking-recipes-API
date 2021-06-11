using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cheesemaking_recipes_API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegistrationDto dto)
        {
            _service.Register(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginDto dto)
        {
            var token = _service.Login(dto);
            return Ok(token);
        }
    }
}
