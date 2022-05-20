using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View();
        }
    }
}
