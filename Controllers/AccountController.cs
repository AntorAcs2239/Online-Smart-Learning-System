using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online__Smart_Learning_System.Models;
using Online__Smart_Learning_System.Models.ViewModel;

namespace Online__Smart_Learning_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result=await signInManager.PasswordSignInAsync(loginVM.UserName!, loginVM.Password!,loginVM.RememberMe,false);
                if (result != null && result.Succeeded)
                {
                    return RedirectToAction("CoursesList", "Courses");
                }
                ModelState.AddModelError("", "Invalid Login");
                return View(loginVM);
            }
            return View(loginVM);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerVM.Email,
                    Email = registerVM.Email,
                    Address = registerVM.Address,
                    Name = registerVM.Name,
                    Role = Request.Form["Role"].ToString()
                };
                var result = await userManager.CreateAsync(user,registerVM.Password!);
                if (result.Succeeded)
                {
                    string role = Request.Form["Role"].ToString();
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                    await userManager.AddToRoleAsync(user, role);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerVM);
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
