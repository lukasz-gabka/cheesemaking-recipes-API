using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("authorize")]
        public ActionResult Authorize()
        {
            /* if user is authenticated, the response status is 200
             * otherwise, the response status is 401 */
            return Ok();
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegistrationDto dto)
        {
            _service.Register(dto);
            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginDto dto)
        {
            var token = _service.Login(dto);
            return Ok(token);
        }
    }
}
