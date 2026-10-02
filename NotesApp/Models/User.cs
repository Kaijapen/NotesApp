#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long.")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters long.")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }

    [NotMapped]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Note> Notes { get; set; } = new List<Note>();
}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string email)
        {
            var context = (MyContext)validationContext.GetService(typeof(MyContext))!;
            bool emailExists = context.Users.Any(u => u.Email == email);
            if (emailExists)
            {
                return new ValidationResult("Email already exists.");
            }
        }
        return ValidationResult.Success;
    }
}