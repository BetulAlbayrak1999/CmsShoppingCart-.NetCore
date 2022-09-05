using AutoMapper;
using BusinessLogic.ViewModels.UserViewModels;
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

        // GET /account/login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginVM loginVM = new LoginVM
            {
                ReturnUrl = returnUrl
            };

            return View(loginVM);
        }

        // POST /account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser appUser = await _userManager.FindByEmailAsync(login.Email);
                    if (appUser != null)
                    {
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                        if (result.Succeeded)
                            return Redirect(login.ReturnUrl ?? "/");
                    }
                    ModelState.AddModelError("", "Login failed, wrong credentials.");
                }
                 
                return View(login);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }

        // GET /account/logout
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return Redirect("/");
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        // GET /account/update
        public async Task<IActionResult> Update()
        {
            try
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                UpdateUserVM user = new UpdateUserVM(appUser);

                return View(user);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        //POST/ account/update
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateUserVM updateUserVM)
        {
            try
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (ModelState.IsValid)
                {
                    appUser.Email = updateUserVM.Email;
                    if (updateUserVM.Password != null)
                    {
                        appUser.PasswordHash = _passwordHasher.HashPassword(appUser, updateUserVM.Password);
                    }

                    IdentityResult result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                        TempData["Success"] = "Your information has been edited!";
                }
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
