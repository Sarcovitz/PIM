using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;

namespace PimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Get([FromQuery]string username)
        {
            var user = await _userService.GetUserByNameAsync(username);
            if (user == null) return NotFound();
            user.Password = "";
            return Ok(user);
        }
    }
}
