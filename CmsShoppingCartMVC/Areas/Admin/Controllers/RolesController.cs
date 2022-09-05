using BusinessLogic.ViewModels.RoleViewModels;
using Entity.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()=> View(_roleManager.Roles);

        // GET /admin/roles/create
        public IActionResult Create() => View();


        // POST /admin/roles/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([MinLength(2), Required] string name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "The role has been created!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors) ModelState.AddModelError("", error.Description);
                    }
                }

                ModelState.AddModelError("", "Minimum length is 2");
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //GET /admin/roles/update/5
        public async Task<IActionResult> Update(string id)
        {
            try
            {
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if (role is not null)
                {
                    List<AppUser> members = new List<AppUser>();
                    List<AppUser> nonMembers = new List<AppUser>();
                    foreach (AppUser user in _userManager.Users) {
                        var list = await _userManager.IsInRoleAsync(user, role.Name)? members: nonMembers;
                        list.Add(user);
                    }
                    return View(new UpdateRoleVM
                    {
                        Role = role,
                        Members = members,
                        NonMembers = nonMembers
                    });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //POST /admin/roles/update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateRoleVM updateRoleVM)
        {
            try
            {
                IdentityResult result;
                foreach (string userId in updateRoleVM.AddIds?? new string[] {}) 
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    result = await _userManager.AddToRoleAsync(user, updateRoleVM.RoleName);
                }

                foreach (string userId in updateRoleVM.DeleteIds?? new string[] {}) 
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    result = await _userManager.RemoveFromRoleAsync(user, updateRoleVM.RoleName);
                }

                return Redirect(Request.Headers["Referer"].ToString());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
