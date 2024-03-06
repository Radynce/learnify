using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace learnify.Controllers;

public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
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
    public ActionResult Login([FromForm]User user)
    {
        var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

        if (dbUser == null)
        {
            TempData["Error"] = "Credentials are wrong. Try Again!";
            return RedirectToAction(nameof(LoginForm));
        }
        if (dbUser.Password != user.Password)
        {
            TempData["Error"] = "Password Wrong?";
            return RedirectToAction(nameof(LoginForm));
        }
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public IActionResult Register([FromForm] User user)
    {
        try
        {
            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(LoginForm));
        }
        catch (System.Exception)
        {
            throw;
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

