using System.ComponentModel.DataAnnotations;

namespace Task8Collection.Models;

public class RegisterViewModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email adress")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}