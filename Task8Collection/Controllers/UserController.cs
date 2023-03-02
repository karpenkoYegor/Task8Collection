using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task8Collection.Models.User;

namespace Task8Collection.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {
            
        }

        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            var context = HttpContext.Request.Form;
            var userId = form["idUserForm"];
            return View();
        }
    }
}
