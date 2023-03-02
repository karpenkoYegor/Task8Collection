using Microsoft.AspNetCore.Identity;
using Task8Collection.Data.Entities;

namespace Task8Collection.Models.Admin;

public class AdminPanelViewModel
{
    //public string GetRoleName(User user)
    //{
    //    var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
    //    return (role != null) ? role : "";
    //}
    public Data.Entities.User User { get; set; }
    public string Role { get; set; }
}