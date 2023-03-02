using Microsoft.AspNetCore.Identity;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<User> GetUsers()
        {
            return Context.Users;
        }

        public IQueryable<IdentityUserClaim<string>> GetUserClaims(string userID)
        {
            return Context.UserClaims.Where(u => u.UserId == userID);
        }

        public IdentityRoleClaim<string> GetClaim(RoleAccount roleAccount)
        {
            return Context.RoleClaims.First(r => r.ClaimValue == roleAccount.ToString());
        }
    }
}
