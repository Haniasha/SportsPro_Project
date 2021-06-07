// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SportsPro.Models;

// <summary>
// User Controller
// This the functionality on how an Admin gives a user permission
// To access the techincidentlink
// An admin is the only one who has access to all links on the navbar
// </summary>

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")] //only allows users of the Admin role to access page. 
    [Area("Admin")]
    public class UserController : Controller
    {
        private UserManager<User> userManager;                       //declare private property.
        private RoleManager<IdentityRole> roleManager;               //declare private property.
        public UserController(UserManager<User> userMngr,            //constructor initializes properties to instances. 
            RoleManager<IdentityRole> roleMngr)
        {
            userManager = userMngr;
            roleManager = roleMngr;
        }

        public async Task<IActionResult> Index()    // calls asynchronous methods. 
        {
            List<User> users = new List<User>();    //creates list named users of user objects.
            foreach (User user in userManager.Users)    //loops through list.
            {
                user.RoleNames = await userManager.GetRolesAsync(user);        //gets list of all role names the user is a member of and assigns to RoleNames property of the User object.
                users.Add(user);
            }
            UserViewModel model = new UserViewModel     //creates user view model and initializes Users and Roles properties. 
            {
                Users = users,
                Roles = roleManager.Roles
            };
            return View(model);     //passes view model to view and renders it. 
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)  //delete action method that takes id parameter
        {
            User user = await userManager.FindByIdAsync(id);  //gets user object that matches id.
            if (user != null)   //check if user object is null.
            {
                IdentityResult result = await userManager.DeleteAsync(user);  //attempts delete if not null.
                if (!result.Succeeded) // if failed
                {
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)  //loops through errors and creates error message and adds to TempData.
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");       //return to home page.
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)      //add to admin action method that takes id parameter
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");    //gets IdentityRole object for the Admin role.
            if (adminRole == null)      //checks if IdentityRole object is null.
            {
                TempData["message"] = "Admin role does not exist. "         //if null adds error message to TempData with message key 
                    + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                User user = await userManager.FindByIdAsync(id);        //if not null, finds User object matching id and adds to Admin role. 
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");   //redirects to Index view. 
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            var result = await userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }
    }
}
