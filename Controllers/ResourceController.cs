using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;

namespace learnify.Controllers;

public class ResourceController : Controller
{
    private readonly AppDbContext _dbContext;
    public ResourceController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IActionResult ViewResource()
    {
        return View();
    }

    public IActionResult AddResource()
    {

        return View();
    }
    [HttpPost]
    public IActionResult SubmitResource([FromForm] Resource resource)
    {
        try
        {
            _dbContext.Resources.Add(resource);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(ViewResource));
        }
        catch
        {
            return RedirectToAction("AddResource");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
