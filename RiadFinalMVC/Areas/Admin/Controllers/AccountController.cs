using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiadFinalMVC.Areas.Admin.ViewModels;

namespace RiadFinalMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateAdmin()
        {
            AppUser user = new ()
            {
                FullName = "Riad Hamidov",
                UserName = "Admin"
            };

            await _userManager.CreateAsync(user, "Admin123@");
            await _userManager.AddToRoleAsync(user, "SuperAdmin");

            return Ok("admin yaradildi");
        }

        public async Task<IActionResult> CreateRoles()
        {
            IdentityRole role = new IdentityRole("Member");
            IdentityRole role2 = new IdentityRole("SuperAdmin");
            IdentityRole role3 = new IdentityRole("Admin");

            await _roleManager.CreateAsync(role);
            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role3);

            return Ok("Rollar yaradildi");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByNameAsync(adminLoginVm.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or password incorrect");
            }

            var result = await _signInManager.PasswordSignInAsync(user, adminLoginVm.Password, adminLoginVm.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password incorrect");
            }

            return RedirectToAction("Index" , "Dashboard");
        }
    }
}
