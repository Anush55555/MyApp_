using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Models.Account;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;

namespace MyApp.Controllers;

public class AccountController : Controller
{
    ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }


    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpUser user)
    {
        //_context.UserSignUp.Add(user);
        //_context.SaveChanges();
        if (ModelState.IsValid)
        {
            var _user = new IdentityUser { UserName = user.Email, PasswordHash = user.Password };
            var result = await  _userManager.CreateAsync(_user, user.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(_user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(" ", error.Description);
            }
        }
        return View();
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
   public async Task<IActionResult> SignIn(SignInUserModel user)
    {
        if (ModelState.IsValid)
        {
            var _user = await _userManager.FindByNameAsync(user.SignInUser.Email);
            if (_user == null)
            {
                return View("UserNotFound");
            }

            var pass = await _userManager.CheckPasswordAsync(user, user.PasswordHash);
            if(pass == false)
            {
                return View("UserNotFound");
            }

            var result = await _signInManager.PasswordSignInAsync(user, user.SignInUser.Password, isPersistent: false, false);
            if(result.Succeeded) 
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.SignInUser.Email)
                };
                var users = await _userManager.GetRolesAsync(user);
                foreach(var user1 in users)
                {
                    claims.Add(new Claim(ClaimTypes.Name, user1));
                }
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
        }
         return View();
        
    }

    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}