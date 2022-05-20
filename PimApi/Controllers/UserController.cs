using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;
using PimModels.Models;

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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string? username, [FromQuery] int? id)
        {
            User? user = null;

            if(id.HasValue) user = await _userService.GetUserByIdAsync(id.Value);
            else if(!string.IsNullOrWhiteSpace(username)) user = await _userService.GetUserByNameAsync(username);

            if (user == null) return NotFound("There is no User under this filter");
            user.Password = "";
            return Ok(user);
        }
    }
}
