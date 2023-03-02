using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;
using Task8Collection.Models.Admin;

namespace Task8Collection.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryWrapper _repository;

        public AdminController(UserManager<User> userManager, IRepositoryWrapper repositoryWrapper)
        {
            _userManager = userManager;
            _repository = repositoryWrapper;
        }
        public IActionResult AdminPanel()
        {
            var model = new List<AdminPanelViewModel>();
            var users = _userManager.Users;
            foreach (var user in users)
            {
                model.Add(new()
                {
                    User = user,
                    Role = user.RoleAccount.ToString()
                });
            }
            return View(model);
        }

        public IActionResult EditPage(string id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            var model = new EditPageViewModel()
            {
                RoleAccount = user.RoleAccount,
                UserId = id,
                UserName = user.UserName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPage(EditPageViewModel model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == model.UserId);
            user.RoleAccount = model.RoleAccount;
            var result = await _userManager.UpdateAsync(user);
            var claimUser = _repository.UserRepository
                .GetUserClaims(model.UserId)
                .Where(c => c.ClaimType == ClaimTypes.Role)
                .Single();
            await _userManager.RemoveClaimAsync(user, claimUser.ToClaim());
            await _userManager.AddClaimAsync(user, _repository.UserRepository.GetClaim(model.RoleAccount).ToClaim());
            
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("AdminPanel");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
