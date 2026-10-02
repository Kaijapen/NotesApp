#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace NotesApp.Models;

public class Note
{
    [Key]
    public int NoteId { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    public string Title { get; set; }

    [Required]
    [MinLength(10, ErrorMessage = "Content must be at least 10 characters long.")]
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}