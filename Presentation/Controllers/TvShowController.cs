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

        [HttpPost]
        public IActionResult AddNewShowsFromApiAsync()
        {
            return Ok(_tvShowService.AddNewShowsFromApiAsync());
        }

        [HttpGet]
        public IActionResult GetByName(string showName)
        {
            var show = _tvShowService.GetByNameAsync(showName);

            if (show == null)
            {
                return NotFound();
            }

            return Ok(_tvShowService.GetByNameAsync(showName));
        }
    }
}