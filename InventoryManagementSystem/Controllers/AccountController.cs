using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRegisterAsync(RegisterViewModel UserViewModel)
        {
            if (ModelState.IsValid)
            {
                //Mapping ViewModel to Model
                ApplicationUser AppUser = new ApplicationUser();
                AppUser.UserName = UserViewModel.UserName;
                AppUser.Email = UserViewModel.Email;
                AppUser.PasswordHash = UserViewModel.Password;

                //save on database
                IdentityResult result = await _userManager.CreateAsync(AppUser, UserViewModel.Password);
                if (result.Succeeded)
                {
                    //assign role to user
                    await _userManager.AddToRoleAsync(AppUser, "User");

                    //cookie
                    await _signInManager.SignInAsync(AppUser, isPersistent: false);
                    return RedirectToAction("Index", "StockTransaction");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }

            return View("Register", UserViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLoginAsync(LoginViewModel UserViewModel)
        {
            if (ModelState.IsValid)
            {
                //check user is valid or not
                ApplicationUser user = await _userManager.FindByNameAsync(UserViewModel.UserName);
                if (user != null)
                {
                    //check password is valid or not
                    bool found = await _userManager.CheckPasswordAsync(user, UserViewModel.Password);
                    if (found)
                    {
                        //create cookie
                        await _signInManager.SignInAsync(user, isPersistent: UserViewModel.RememberMe);

                        return RedirectToAction("Index", "StockTransaction");
                    }
                }
                ModelState.AddModelError("", "Invalid UserName or Password");
            }
            return View("Login", UserViewModel);
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSignOut(SignOutViewModel UserViewModel)
        {
            if (UserViewModel.IsSignedOut)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveChangePasswordAsync(ChangePasswordViewModel UserViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.FindByNameAsync(UserViewModel.Name).Result;
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, UserViewModel.OldPassword);
                    if (found)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, UserViewModel.OldPassword, UserViewModel.NewPassword);
                        if (result.Succeeded)
                        {
                            await _signInManager.SignOutAsync();
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Old Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User not found");
                }
            }
            return View("ChangePassword", UserViewModel);
        }
    
        [HttpGet]
        public async Task<IActionResult> DetailsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
