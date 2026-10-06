#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace NotesApp.Models;

public class LoginUser
{
    [Required]
    [EmailAddress]
    public string LoginEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; }

    [Display(Name = "Stay signed in?")]
    public bool StaySignedIn { get; set; } = false;
}