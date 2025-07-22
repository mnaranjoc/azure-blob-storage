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
            var containers = await _containerService.GetAllContainerAndBlobs();
            return View(containers);
        }

        public async Task<IActionResult> Create(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                await _containerService.CreateContainer(name);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string name)
        {
            await _containerService.DeleteContainer(name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBlob(string name, string containerName)
        {
            await _containerService.DeleteBlob(name, containerName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PreviewBlob(string name, string containerName)
        {
            await _containerService.PreviewBlob(name, containerName);
            return Redirect(await _containerService.PreviewBlob(name, containerName));
        }

        public async Task<IActionResult> CreateBlob(IFormFile file, string containerName)
        {
            if (file != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                await _containerService.CreateBlob(fileName, containerName, file);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateBlobMetadata(string name, string containerName)
        {
            await _containerService.UpdateBlobMetadata(name, containerName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBlobMetadata(string name, string containerName)
        {
            await _containerService.DeleteBlobMetadata(name, containerName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PreviewWithSasToken(string name, string containerName)
        {
            var publicURL = await _containerService.GeneratePublicURL(name, containerName);
            return Redirect(publicURL.AbsoluteUri);
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
