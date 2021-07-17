using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewnetCore.Models;
using NewnetCore.ViewModels;

namespace NewnetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        // private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            //this.userManager = userManager;
            //SignInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task <IActionResult> IsEmailInuse (string email)
        {
          var user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }
        //[AllowAnonymous]
        //public async Task<IActionResult> Register(RegistrationViewModel model)
        [HttpPost]
        [AllowAnonymous]
        public async Task <IActionResult> Register (RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email,
                City = model.City
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
      [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LogingViewModel model, string returnUrl)
        { 
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await signInManager.PasswordSignInAsync(model.Email,model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                   
                }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }
    }
}
