using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;
using Microsoft.EntityFrameworkCore;

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
#pragma warning disable
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext.Session.GetString("User") != null)
#pragma warning restore
        {
            return View();
        }
        TempData["NotLoggedIn"] = "Login to add your resources.";
        return RedirectToAction("LoginForm", "User");
    }


    [HttpPost]
    public IActionResult SubmitResource([FromForm] Resource resource)
    {
        if (ModelState.IsValid)
        {
            try
            {
#pragma warning disable
                var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
                resource.UserId = Guid.Parse(userId);
#pragma warning restore
                _dbContext.Resources.Add(resource);
                _dbContext.SaveChanges();
                TempData["Success"] = "Whoa! captain thanks for your contribution.";
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


    //Delete
    [HttpPost]
    public IActionResult Delete(Guid Id)
    {
        var queryId = Guid.TryParse(Request.Query["itemid"], out var guidId) ? guidId : Guid.Empty;
        var resource = _dbContext.Resources.FirstOrDefault(r => r.Id == queryId);
        Console.WriteLine(queryId);

        if (resource == null)
        {
            Console.WriteLine("No resource found.");
            TempData["Error"] = "No resource available";
            return RedirectToAction("Index","Home");
        }
        Console.WriteLine("Deleting resource");
        _dbContext.Resources.Remove(resource);
        _dbContext.SaveChanges();
        Console.WriteLine("Resource deleted");
        return RedirectToAction("ActivityManager", "Session");

    }

    public IActionResult Edit([FromForm] Guid Id)
    {
        var queryId = Guid.TryParse(Request.Query["itemid"], out var guidId) ? guidId : Guid.Empty;
        Console.WriteLine(queryId);
        var resource = _dbContext.Resources.FirstOrDefault(r => r.Id == queryId);
        if (resource == null)
        {
            Console.WriteLine("No resource found.");
            TempData["Error"] = "No resource available";
            return RedirectToAction("Index","Home");
        }
        return View("EditResource",resource);    

    }
    public ActionResult ConfirmEdit([FromForm]Resource resource, Guid id){
        if (ModelState.IsValid)
        {
            try
            {
#pragma warning disable
                var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
                resource.UserId = Guid.Parse(userId);
#pragma warning restore
                _dbContext.Resources.Update(resource);
                _dbContext.SaveChanges();
                TempData["Success"] = "Resource updated successfully";
                return RedirectToAction("ActivityManager","Session");
            }
            catch
            {
                return RedirectToAction("EditResource");
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
