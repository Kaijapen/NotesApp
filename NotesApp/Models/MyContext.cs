#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace NotesApp.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }
}