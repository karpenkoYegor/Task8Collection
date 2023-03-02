using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Task8Collection.Data.Entities;

namespace Task8Collection.Data;

public class DataSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public DataSeeder(UserManager<User> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }
    public void Seed()
    {
        _dbContext.Database.EnsureCreated();

        var hasher = new PasswordHasher<User>();
        var passwordHash = hasher.HashPassword(null, "PasswordAdmin123");

        if (!_dbContext.Users.Any())
        {
            var user = new User
            {
                Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                UserName = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                PasswordHash = passwordHash,
                SecurityStamp = "",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                RoleAccount = RoleAccount.Admin
            };
            _userManager.CreateAsync(user, "PasswordAdmin123").GetAwaiter().GetResult();
            var adminGuid = Guid.NewGuid().ToString();
            var userGuid = Guid.NewGuid().ToString();
            _dbContext.Roles.AddRange(new List<IdentityRole>()
            {
                new IdentityRole() { Id = adminGuid, Name = "Admin" },
                new IdentityRole() { Id = userGuid, Name = "User" }
            });
            _dbContext.RoleClaims.AddRange(new List<IdentityRoleClaim<string>>()
            {
                new IdentityRoleClaim<string>() { RoleId = adminGuid, ClaimType = ClaimTypes.Role, ClaimValue = "Admin" },
                new IdentityRoleClaim<string>() { RoleId = userGuid, ClaimType = ClaimTypes.Role, ClaimValue = "User" }
            });
            _dbContext.Themes.AddRange(new List<Theme>()
            {
                new Theme()
                {
                    Name = "Фишки",
                },
                new Theme()
                {
                    Name = "Карточки",
                },
                new Theme()
                {
                    Name = "Марки",
                },
                new Theme()
                {
                    Name = "Фигурки"
                },
                new Theme()
                {
                    Name = "Вещи в доте"
                },
                new Theme()
                {
                    Name = "Виниловые диски"
                },
                new Theme()
                {
                    Name = "Винтажные вещи"
                },
            });
            _dbContext.SaveChanges();
            _userManager.AddClaimAsync(user, _dbContext.RoleClaims.First(c => c.ClaimValue == "Admin").ToClaim()).GetAwaiter().GetResult();
        }
    }
}