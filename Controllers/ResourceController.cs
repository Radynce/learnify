using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;

namespace learnify.Controllers;

public class ResourceController : Controller
{
    private readonly ILogger<ResourceController> _logger;

    public ResourceController(ILogger<ResourceController> logger)
    {
        _logger = logger;
    }

    public IActionResult ViewResource()
    {
        return View();
    }

    public IActionResult AddResource(){
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
