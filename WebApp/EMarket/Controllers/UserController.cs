using Microsoft.AspNetCore.Mvc;
using EMarket.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using EMarket.Core.Application.Helpers;
using EMarket.Core.Application.Interfaces.Services;
using WebApp.EMarket.Middlewares;

namespace EMarket.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            UserViewModel userVm = await _userService.Login(vm);

            if (userVm != null)
            {
                HttpContext.Session.Set("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Invalid user credentials.");
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _userService.Add(vm);

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
