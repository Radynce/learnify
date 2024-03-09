
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;

public class AdminController : Controller {

    private readonly IHttpContextAccessor _httpContext;
    private readonly AppDbContext _context;
    [ActivatorUtilitiesConstructor] //solve for multiple constructors
    public AdminController(AppDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public ActionResult DeleteUser(Guid Id){

        var queryId = Guid.TryParse(Request.Query["userid"], out var guidId) ? guidId : Guid.Empty;
        var user = _context.Users.FirstOrDefault(u => u.UserId == queryId);

        if (user == null)
        {
            TempData["Error"] = "No resource available";
            return RedirectToAction("GetUser", "User");
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
        return RedirectToAction("GetUser", "User");
    }
}