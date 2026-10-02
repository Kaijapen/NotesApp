using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NotesApp.Models;

namespace NotesApp.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private MyContext _context;
    
    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet("User/Login")]
    public IActionResult Login()
    {
        return View();
    }

    // Post request to create a new user
    [HttpPost("users/create")]
    public IActionResult RegisterUser(User newUser)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        // Hash the user password before saving to the database
        PasswordHasher<User> hasher = new PasswordHasher<User>();

        newUser.Password = hasher.HashPassword(newUser, newUser.Password);

         _context.Users.Add(newUser);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("UserId", newUser.UserId);

        return RedirectToAction("Dashboard", "Notes");
    }

    // Post request to login a user
    [HttpPost("users/login")]
    public IActionResult LoginUser(LoginUser loginUser)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        // check if the user exists in the database
        User? userInDb = _context.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);

        // if the user does not exist, return to the login page with an error message
        if (userInDb == null)
        {
            ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
            return View("Login");
        }

        // if the user exists, verify the password
        PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

        // verify the hashed password
        var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LoginPassword);

        // if the password is incorrect, return to the login page with an error message
        if (result == 0)
        {
            ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
            return View("Index");
        }

        // if the password is correct, set the user id in session and redirect to the dashboard
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);

        return RedirectToAction("Dashboard", "Notes");
    }

    // Get request to logout a user
    [HttpGet("users/logout")]
    public IActionResult Logout()
    {
        // Clear the session and redirect to the login page
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

}
