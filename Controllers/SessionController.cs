using Microsoft.AspNetCore.Mvc;
using learnify.Models;
public class SessionController : Controller
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly AppDbContext _context;
    [ActivatorUtilitiesConstructor] //solve for multiple constructors
    public SessionController(AppDbContext context, IHttpContextAccessor httpContext)
    {
        _context= context;
        _httpContext = httpContext;
    }
    public IActionResult ViewProfileActivity()
    {
        return View();
    }

    public ActionResult Logout()
    {
        _httpContext.HttpContext.Session.Remove("User");
        _httpContext.HttpContext.Session.Remove("UserId");
        TempData["Logout Success"] = "User logged out successfully";
        return RedirectToAction("Index","Home");
    }

    public ActionResult ActivityManager(){
        var user = _httpContext.HttpContext.Session.GetString("UserId");
        Console.WriteLine("moved to activity");
        return RedirectToAction("Index","Home");
    }
}
