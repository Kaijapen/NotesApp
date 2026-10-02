using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;

namespace NotesApp.Controllers;

public class NotesController : Controller
{
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
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}