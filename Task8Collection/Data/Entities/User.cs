using Microsoft.AspNetCore.Identity;

namespace Task8Collection.Data.Entities;

public class User : IdentityUser
{
    public RoleAccount RoleAccount { get; set; }
    public List<Collection> Collections { get; set; }
    public List<LikedItems> LikedItems { get; set; }
    public ThemeUi ThemeUi { get; set; }
    public LanguageUi LanguageUi { get; set; }
}

public enum RoleAccount
{
    Admin,
    User
}

public enum ThemeUi
{
    Light,
    Dark
}

public enum LanguageUi
{
    En,
    Ru
}