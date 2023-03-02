using Microsoft.AspNetCore.Identity;
using Task8Collection.Data.Entities;

namespace Task8Collection.Data.Repository.Intefaces;

public interface IUserRepository : IRepositoryBase<User>
{
    IQueryable<User> GetUsers();
    IQueryable<IdentityUserClaim<string>> GetUserClaims(string userID);
    IdentityRoleClaim<string> GetClaim(RoleAccount roleAccount);
}