using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;

namespace learnify.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string LoggedInUser = TempData["LoggedInUser"] as string;
        if(TempData != null){
            ViewBag.LoggedInUser = LoggedInUser;
        }
       return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
