using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;

namespace learnify.Controllers;

public class UserController : Controller
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly AppDbContext _context;
    [ActivatorUtilitiesConstructor] //solve for multiple constructors
    public UserController(AppDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public IActionResult RegisterForm()
    {
        return View();
    }
    public ActionResult LoginForm()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login([FromForm] User user)
    {
        var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

        if (dbUser == null)
        {
            TempData["Error"] = "No user found, try batman!";
            return RedirectToAction(nameof(LoginForm));
        }
        if (dbUser.Password != user.Password)
        {
            TempData["Error"] = "Remember when she said, you are the one.";
            return RedirectToAction(nameof(LoginForm));
        }
        TempData["Success"] = "I'd say god bless you, but it looks like he already did.";
        TempData["LoggedInUser"] = dbUser?.Username ?? "null";
#pragma warning disable
        _httpContext.HttpContext.Session.SetString("UserType", dbUser?.UserType);
        _httpContext.HttpContext.Session.SetString("User", dbUser?.Username);
        _httpContext.HttpContext.Session.SetString("UserId", dbUser?.UserId.ToString());
#pragma warning restore
        return RedirectToAction("Index", "Home");
    }


    [HttpPost]
    public IActionResult Register([FromForm] User user)
    {
        try
        {
            if (!string.IsNullOrEmpty(user.Username) &&
                !string.IsNullOrEmpty(user.FullName) &&
                !string.IsNullOrEmpty(user.Email) &&
                !string.IsNullOrEmpty(user.Password))
            {
                user.UserType = "User";
                user.CreatedAt = DateTime.UtcNow;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(LoginForm));
            }
            else
            {
                TempData["Error"] = "Please fill up all sections.";
                return RedirectToAction(nameof(RegisterForm));
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    // Delete Resource
    public IActionResult Delete()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

