using Microsoft.AspNetCore.Mvc;
using DAL.Services;
using DAL.Entities;

namespace TrainLink.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var user = _userService
                .GetByEmail(username)
                .FirstOrDefault();
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View();
            }
            if (user.Password != password)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View();
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
