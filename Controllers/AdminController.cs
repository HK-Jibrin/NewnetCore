using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewnetCore.Models;
using NewnetCore.ViewModels;

namespace NewnetCore.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser>userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

               IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles","Admin");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
             return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        public async Task<IActionResult>EditRole(string id)
        {
           var role = await roleManager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot befound";
                return View("NotFound");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name

            };
           foreach(var user in userManager.Users)
            {
               if(await userManager.IsInRoleAsync(user, role.Name)){
                    model.Users.Add(user.UserName);

                }
            }
            return View(model);

        }

    }
}
