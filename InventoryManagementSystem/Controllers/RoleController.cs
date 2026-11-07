using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View("AddRole");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleViewModel.Name;
               IdentityResult result = await _roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    ViewBag.success = true;
                    return View("AddRole");
                }
                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
            }
            return View("AddRole", roleViewModel);
        }
    }
}
