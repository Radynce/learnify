using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("/[controller]")]
public class UserController : Controller
{
    private readonly AppDbContext? _db;

    public UserController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid Id)
    {
        return View(await _db.Users.ToListAsync());
    }

    [HttpPost("{addUser}")]
    public async Task<ActionResult<User>> RegisterUser(User user)
    {
        user.Username = "kshetritej";
        user.Email = "email@kshetritej.com.np";
        user.FullName = "Tej Bahadur Gharti Kshetri";
        user.Password = "jagadamba@23";
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        Console.WriteLine("user added");
        return View(user);

    }
}
