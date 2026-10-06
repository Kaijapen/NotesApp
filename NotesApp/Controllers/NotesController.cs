using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;

namespace NotesApp.Controllers;

public class NotesController : Controller
{
    public int? id
    {
        get
        {
            return HttpContext.Session.GetInt32("UserId");
        }
    }
    private readonly ILogger<NotesController> _logger;
    private MyContext _context;
    
    public NotesController(ILogger<NotesController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("notes/dashboard")]
    public IActionResult Dashboard()
    {
        if (id == null)
        {
            return RedirectToAction("Login", "User");
        }
        ViewBag.User = _context.Users.FirstOrDefault(u => u.UserId == id);
        ViewBag.Notes = _context.Notes.Where(n => n.UserId == id).ToList();
        return View();
    }

    [HttpGet("notes/new")]
    public IActionResult Create()
    {
        if (id == null)
        {
            return RedirectToAction("Login", "User");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}