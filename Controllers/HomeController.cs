using EventProject.Core;
using EventProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        [HttpPost]
        public IActionResult SaveEventData([FromBody] EventData eventData)
        {
            _logger.LogInformation( $"Some value: {eventData.SomeValue} .  Some text: {eventData.Text}");

            return Ok();
        }
        [HttpGet]
        public IActionResult GetValues()
        {
           var data = _homeService.GetValuesFromLog();
            return new JsonResult(data);

        }

        public IActionResult Index()
        {
            return View();
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}