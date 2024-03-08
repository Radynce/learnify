using Microsoft.AspNetCore.Mvc;
public class SessionController : Controller
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly AppDbContext _context;
    [ActivatorUtilitiesConstructor] //solve for multiple constructors
    public SessionController(AppDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }
    public IActionResult ViewProfileActivity()
    {
        return View();
    }

    public ActionResult Logout()
    {
#pragma warning disable
        _httpContext.HttpContext.Session.Remove("User");
        _httpContext.HttpContext.Session.Remove("UserId");
        _httpContext.HttpContext.Session.Remove("UserType");
#pragma warning restore
        TempData["Logout Success"] = "User logged out successfully";
        return RedirectToAction("Index", "Home");
    }

    public ActionResult ActivityManager()
    {
        var user = _httpContext.HttpContext.Session.GetString("UserId");
        var userType = _httpContext.HttpContext.Session.GetString("UserType");
        var userId = Guid.Parse(user);
        var items = _context.Resources.Where(r => r.UserId == userId).ToList();
        return View(items);
    }
}
