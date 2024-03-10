
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;

public class AdminController : Controller {

    private readonly IHttpContextAccessor _httpContextAccessorAccessor;
    private readonly AppDbContext _context;
    [ActivatorUtilitiesConstructor] //solve for multiple constructors
    public AdminController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessorAccessor = httpContextAccessor;
    }

    public ActionResult DeleteUser(Guid Id){

#pragma warning disable
        var httpContext = _httpContextAccessorAccessor.HttpContext;
        if (httpContext.Session.GetString("User") == null)
#pragma warning restore
        {
            TempData["LoginToDelete"] = "You must login first to perform that action.";
            return RedirectToAction("LoginForm","User");
        }
        var queryId = Guid.TryParse(Request.Query["userid"], out var guidId) ? guidId : Guid.Empty;
        var user = _context.Users.FirstOrDefault(u => u.UserId == queryId);

        if (user == null)
        {
            TempData["Error"] = "No resource available";
            return RedirectToAction("GetUser", "User");
        }
        #pragma warning disable
        if(user.Username == httpContext.Session.GetString("User")){
            Console.WriteLine("Life one shot ho, marera feri janmey jhan arko jhanjhat ho!");
            TempData["SelfDestroy"] = "You can't delete yourself. ";
            return RedirectToAction("GetUser","User");
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
        return RedirectToAction("GetUser", "User");
    }
    #pragma warning restore

    public ActionResult ManageResource(){
#pragma warning disable
        var httpContext = _httpContextAccessorAccessor.HttpContext;
        if (httpContext.Session.GetString("User") == null)
#pragma warning restore
        {
            TempData["LoginToManage"] = "You must login first to perform that action.";
            return RedirectToAction("LoginForm","User");
        }
        var resources = _context.Resources.ToList();
        return View(resources);
    }
}