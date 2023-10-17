using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TvShowController : ControllerBase
    {
        private readonly ITvShowService _tvShowService;
        private readonly ILogger<TvShowController> _logger;

        public TvShowController(
            ITvShowService tvShowService,
            ILogger<TvShowController> logger)
        {
            _tvShowService = tvShowService;
            _logger = logger;
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByNameAsync(string showName)
        {
            var show = await _tvShowService.GetByName(showName);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _tvShowService.GetAll());
        }
    }
}