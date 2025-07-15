using System.Diagnostics;
using AzureBlobStorageWebApp.Models;
using AzureBlobStorageWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobStorageWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContainerService _containerService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IContainerService containerService, ILogger<HomeController> logger)
        {
            _containerService = containerService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var containers = await _containerService.GetAllContainer();
            return View(containers);
        }

        public IActionResult Privacy()
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
