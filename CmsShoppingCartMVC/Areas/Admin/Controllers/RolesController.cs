using Entity.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Areas.Admin.Controllers
{
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
    }
}
