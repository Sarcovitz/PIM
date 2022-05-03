using PimApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimModels.RequestModels;
using PimModels.Models;

namespace PIM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            if (!ModelState.IsValid || loginUser is null) return BadRequest(ModelState);

            return Ok(await _authenticationService.LoginAsync(loginUser));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            if (!ModelState.IsValid || registerUser is null) return BadRequest(ModelState);

            await _authenticationService.RegisterAsync(registerUser);

            return Ok("Success.");
        }

        [HttpGet]
        public async Task<IActionResult> CurrentUser()
        {
            int userId = Convert.ToInt32(User.FindFirst("Id").Value);

            User? user = await _authenticationService.GetCurrentUserAsync(userId);
            if (user is not null)
            {
                user.Password = "";
                return Ok(user);
            }
            else return NotFound();
        }
    }
}
