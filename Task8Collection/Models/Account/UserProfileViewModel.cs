using System.ComponentModel.DataAnnotations;
using Task8Collection.Data.Entities;

namespace Task8Collection.Models.Account;

public class UserProfileViewModel
{
    public Data.Entities.User? User { get; set; }
    public List<Data.Entities.Collection>? UserCollection { get; set; }
    [Required]
    public string NameNewCollection { get; set; }
    public List<Theme>? Themes { get; set; }
    public Guid ThemeId { get; set; }
    [Required]
    public string Description { get; set; }
}