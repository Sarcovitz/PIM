using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;

namespace PimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll() => Ok(await _currencyService.GetAllAsync());
    }
}
