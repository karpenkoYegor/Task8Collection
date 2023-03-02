using Task8Collection.Data.Entities;

namespace Task8Collection.Models.Admin;

public class EditPageViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public RoleAccount RoleAccount { get; set; }
}