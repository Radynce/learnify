using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learnify.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

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
    public ActionResult LoginForm(){
        return View();
    }
    public ActionResult Login(){
        return View(nameof(RegisterForm));
    }
    [HttpPost]
    public IActionResult Register([FromForm] User user){
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
