using AutoMapper;
using Entity.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private IPasswordHasher<AppUser> _passwordHasher;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                IPasswordHasher<AppUser> passwordHasher,
                                IMapper mapper)
        {
            
            _userManager = userManager; 
            _signInManager = signInManager; 
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }


        // GET /account/register
        [AllowAnonymous]
        public IActionResult Register() => View();

        // POST /account/register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser appUser = _mapper.Map<AppUser>(user);
                    IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                    if (result.Succeeded)
                        return RedirectToAction("Login");
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }

                return View(user);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }
    }
}
