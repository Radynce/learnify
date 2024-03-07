using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;

namespace learnify.Controllers;

public class ResourceController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    [ActivatorUtilitiesConstructor] //solve for multiple constructors
    public ResourceController(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    public IActionResult ViewResource()
    {
        var resources = _dbContext.Resources.ToList();
        return View(resources);
    }

    public IActionResult AddResource()
    {

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext.Session.GetString("User") != null)
        {
            return View();
        }
        TempData["NotLoggedIn"] = "Login to add your resources.";
        return RedirectToAction("LoginForm","User");
    }


    [HttpPost]
    public IActionResult SubmitResource([FromForm] Resource resource)
    {
        if(ModelState.IsValid){
        try
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            resource.UserId = Guid.Parse(userId);
            _dbContext.Resources.Add(resource);
            _dbContext.SaveChanges();
            TempData["Success"] ="Whoa! captain thanks for your contribution.";
            return RedirectToAction(nameof(ViewResource));
        }
        catch
        {
            return RedirectToAction("AddResource");
        }
 
        }
        TempData["Error"] = "Error inserting data, try again";
        return View();
   }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
