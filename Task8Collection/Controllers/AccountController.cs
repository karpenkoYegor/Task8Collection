using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;
using Task8Collection.Models;
using Task8Collection.Models.Account;

namespace Task8Collection.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepositoryWrapper _repository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IRepositoryWrapper repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                RoleAccount = RoleAccount.User
            };

            var claimRole = new Claim(ClaimTypes.Role, RoleAccount.User.ToString());

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, claimRole);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var result = await _signInManager.PasswordSignInAsync(model.Email, 
                model.Password, 
                model.RememberMe, 
                false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out.");
                }
                else
                {
                    ModelState.AddModelError("Login", "Failed to login.");
                }

                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> UserProfile(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var model = new UserProfileViewModel
                {
                    User = user,
                    UserCollection = _repository.CollectionRepository.GetCollectionByUserId(user.Id).ToList(),
                    Themes = _repository.ThemeRepository.GetAll().ToList()
                };
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeTheme(string theme, string returnUrl)
        {
            Response.Cookies.Append(
                "theme",
                theme,
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return RedirectToAction("UserProfile", new { userName = User.Identity.Name });
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl, string userName)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return RedirectToAction("UserProfile", new { userName = User.Identity.Name });
        }
    }
}
