using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Models.Account;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace MyApp.Controllers;

public class AccountController : Controller
{
    ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
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
   public IActionResult SignIn(SignInUserModel user)
    {
        if (ModelState.IsValid)
        {
            
        }
        return RedirectToAction("Index", "Home");
    }

}